using System;
using UnityEngine;
using System.Xml;
using System.Reflection;
using ChunkGame.Attribute;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ChunkGame.Assets
{
    public class GameObjectAsset : IAsset<GameObject>
    {
        private const string ObjectName = "Xml_GameObjectName";

        private const string CompAssembly = "Assembly";
        private const string CompName = "Name";

        public GameObject getAsset { get; private set; }

        public string Url { get; private set; }
        private string LocalDirUrl;

        public XmlDocument xml;

        public GameObjectAsset(string xmlPath)
        {
            LocalDirUrl = Path.GetDirectoryName(xmlPath);
            Url = xmlPath;
            xml = new XmlDocument();
            xml.Load(xmlPath);
        }

        public void Read(Action<GameObject> func)
        {
            XmlNode root = xml.SelectSingleNode("GameObject");
            GameObject Obj;
            RestoreGameObject(root as XmlElement, out Obj);
            func(Obj);
            LoadImport();
        }
        /// <summary>
        /// 还原物体
        /// </summary>
        /// <param name="xmle"></param>
        /// <param name="parent"></param>
        private void RestoreGameObject(XmlElement xmle,out GameObject obj)
        {
            string name = xmle.GetAttribute(ObjectName);
            if (name == null || name.Equals(""))
            {
                name = "GameObject";
            }
            obj = new GameObject(name);
            
            RestoreComponent(xmle, obj);
            RestoreImport(xmle, obj);
            XmlNodeList nodeList = xmle.SelectNodes("GameObject");
            foreach (XmlElement item in nodeList)
            {
                GameObject child;
                RestoreGameObject(item, out child);
                child.transform.SetParent(obj.transform);
            }

        }

        /// <summary>
        /// 还原组件
        /// </summary>
        /// <param name="xmle"></param>
        /// <param name="obj"></param>
        private void RestoreComponent(XmlElement xmle, GameObject obj)
        {
            XmlNodeList nodeList = xmle.SelectNodes("Component");
            foreach (XmlElement item in nodeList)
            {
                string assembly = item.GetAttribute(CompAssembly);
                string compName = item.GetAttribute(CompName);
                Type comType;
                if (assembly.Equals(""))
                {
                    comType = Type.GetType(compName, true);
                }
                else
                {
                    Assembly _assembly = Assembly.Load(assembly);
                    comType = _assembly.GetType(compName, true);
                }
                Component com = obj.AddComponent(comType);
                SetField(item, com, comType);
            }
        }
        /// <summary>
        /// 还原引用的其他物体
        /// </summary>
        /// <param name="xmle"></param>
        /// <param name="obj"></param>
        private void RestoreImport(XmlElement xmle, GameObject obj)
        {
            XmlNodeList nodeList = xmle.SelectNodes("Import");
            foreach (XmlElement item in nodeList)
            {
                string import = item.GetAttribute("Path");
                import = import.Replace("{$}", LocalDirUrl + "/");
                //记录物体Xml及其父物体
                ImportQueue.Enqueue(new ImportObj { path = import, parent = obj, });
            }
        }
        /// <summary>
        /// 还原字段值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="com"></param>
        /// <param name="comType"></param>
        private void SetField(XmlElement xmle, Component com, Type comType)
        {
            XmlElement comParams = xmle.SelectSingleNode("Params") as XmlElement;
            foreach (XmlAttribute item in comParams.Attributes)
            {
                string fieldName = item.Name;
                object fieldValue = item.Value;
                FieldInfo field = comType.GetField(fieldName);
                if (field != null)
                {
                    fieldValue = ConvertToObject(fieldValue, field.FieldType);
                    field.SetValue(com, fieldValue);
                }
            }
        }

        /// <summary>
        /// 将一个对象转换为指定类型
        /// </summary>
        /// <param name="obj">待转换的对象</param>
        /// <param name="type">目标类型</param>
        /// <returns>转换后的对象</returns>
        private object ConvertToObject(object obj, Type type)
        {
            if (type == null) return obj;
            if (obj == null) return type.IsValueType ? Activator.CreateInstance(type) : null;

            Type underlyingType = Nullable.GetUnderlyingType(type);
            if (type.IsAssignableFrom(obj.GetType())) // 如果待转换对象的类型与目标类型兼容，则无需转换
            {
                return obj;
            }
            else if ((underlyingType ?? type).IsEnum) // 如果待转换的对象的基类型为枚举
            {
                if (underlyingType != null && string.IsNullOrEmpty(obj.ToString())) // 如果目标类型为可空枚举，并且待转换对象为null 则直接返回null值
                {
                    return null;
                }
                else
                {
                    return Enum.Parse(underlyingType ?? type, obj.ToString());
                }
            }
            else if (typeof(IConvertible).IsAssignableFrom(underlyingType ?? type)) // 如果目标类型的基类型实现了IConvertible，则直接转换
            {
                try
                {
                    return Convert.ChangeType(obj, underlyingType ?? type, null);
                }
                catch
                {
                    return underlyingType == null ? Activator.CreateInstance(type) : null;
                }
            }
            else
            {
                System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(type);
                if (converter.CanConvertFrom(obj.GetType()))
                {
                    return converter.ConvertFrom(obj);
                }
                ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
                if (constructor != null)
                {
                    object o = constructor.Invoke(null);
                    PropertyInfo[] propertys = type.GetProperties();
                    Type oldType = obj.GetType();
                    foreach (PropertyInfo property in propertys)
                    {
                        PropertyInfo p = oldType.GetProperty(property.Name);
                        if (property.CanWrite && p != null && p.CanRead)
                        {
                            property.SetValue(o, ConvertToObject(p.GetValue(obj, null), property.PropertyType), null);
                        }
                    }
                    return o;
                }
            }
            return obj;
        }


        private class ImportObj
        {
            public string path;
            public GameObject parent;
        }

        private Queue<ImportObj> ImportQueue = new Queue<ImportObj>();

        private void LoadImport()
        {
            while (ImportQueue.Count > 0)
            {
                ImportObj o = ImportQueue.Dequeue();
                GameObjectAsset im = new GameObjectAsset(o.path);
                im.Read((obj) => { obj.transform.SetParent(o.parent.transform); });
            }
        }

    }
}
