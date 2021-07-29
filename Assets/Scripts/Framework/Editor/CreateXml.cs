using ChunkGame.Attribute;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Xml;
using ChunkGame.Assets;
using YamlDotNet.Serialization;
public class CreateXml
{
    private const string ObjectName = "Xml_GameObjectName";

    private const string CompAssembly = "Assembly";
    private const string CompName = "Name";

    public static string SavePath = Application.dataPath + @"/Resources/ObjectXml/";
    private static XmlDocument config;
    [MenuItem(@"Assets/Create Prefab Xml",false,1)]
    public static void CreateXmlConfig()
    {
        GameObject obj = Selection.activeGameObject;
        if (obj == null)
        {
            return;
        }
        if (!System.IO.Directory.Exists(SavePath))
        {
            System.IO.Directory.CreateDirectory(SavePath);
        }
        config = new XmlDocument();
        XmlElement parent = config.CreateElement("GameObject");
        config.AppendChild(parent);
        GetObjectData(parent, obj);
        config.Save(SavePath + obj.name + ".xml");
        Debug.Log("Xml Create Finish :" + obj.name);
    }
    /// <summary>
    /// 写入节点
    /// </summary>
    /// <param name="xmlE"></param>
    /// <param name="obj"></param>
    private static void GetObjectData(XmlElement xmlE, GameObject obj)
    {
        xmlE.SetAttribute(ObjectName, obj.name);
        //记录组件
        Component[] comps = obj.GetComponents(typeof(Component));
        for (int i = 0; i < comps.Length; i++)
        {
            if (comps[i].GetType().IsAssignableFrom(typeof(Transform)))
            {
                continue;
            }
            XmlElement comp = config.CreateElement("Component");
            comp.SetAttribute(CompAssembly, comps[i].GetType().Assembly.GetName().Name);
            comp.SetAttribute(CompName, comps[i].GetType().FullName);
            WriteValue(comp, comps[i],comps[i].GetType());
            xmlE.AppendChild(comp);
        }
        //记录子物体
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            XmlElement child = config.CreateElement("GameObject");
            GetObjectData(child, obj.transform.GetChild(i).gameObject);
            xmlE.AppendChild(child);
        }
    }
    /// <summary>
    /// 写入打上ToXml标签的值
    /// </summary>
    /// <param name="xmlE"></param>
    /// <param name="comp"></param>
    private static void WriteValue(XmlElement xmlE, Component comp, Type compType)
    {
        XmlElement compParams = config.CreateElement("Params");
        foreach (var item in compType.GetFields())
        {
            var atts = item.GetCustomAttributes(typeof(ToXml),false);
            if (atts.Length > 0)
            {
                string key = item.Name;
                object value = item.GetValue(comp);
                compParams.SetAttribute(key, value.ToString());
            }
        }
        xmlE.AppendChild(compParams);
    }
}
