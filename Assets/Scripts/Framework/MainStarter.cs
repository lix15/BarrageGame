using System;
using System.Collections;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using ChunkGame.Assets;
using System.Text;
using UnityEngine.Events;

namespace ChunkGame
{
    public class MainStarter:MonoBehaviour
    {
        #region Update事件
        public enum EventType
        {
            Update = 0,
            FixedUpdate, 
            LateUpdate,
        }
        public static Dictionary<EventType, UnityAction> EventDic = new Dictionary<EventType, UnityAction>();

        [XLua.LuaCallCSharp]
        public static void RegisterEvent(UnityAction func, int eventType = 0)
        {
            EventType et = (EventType)eventType;
            if (EventDic.ContainsKey(et))
            {
                EventDic[et] += func;
            }
            else
            {
                EventDic[et] = func;
            }
        }

        [XLua.LuaCallCSharp]
        public static void RemoveEvent(UnityAction func, int eventType = 0)
        {
            EventType et = (EventType)eventType;
            if (EventDic.ContainsKey(et))
            {
                EventDic[et] -= func;
            }
        }

        [XLua.LuaCallCSharp]
        public static void ClearEvent(int eventType = 0)
        {
            EventType et = (EventType)eventType;
            if (EventDic.ContainsKey(et))
            {
                EventDic.Remove(et);
            }
        }

        private void Update()
        {
            if (EventDic.ContainsKey(EventType.Update))
            {
                EventDic[EventType.Update]?.Invoke();
            }
        }

        private void FixedUpdate()
        {
            if (EventDic.ContainsKey(EventType.FixedUpdate))
            {
                EventDic[EventType.FixedUpdate]?.Invoke();
            }
        }

        private void LateUpdate()
        {
            if (EventDic.ContainsKey(EventType.LateUpdate))
            {
                EventDic[EventType.LateUpdate]?.Invoke();
            }
        }
        #endregion


        public string StarterPath;
        private void Awake()
        {
            RegisterEvent(Lua.LuaBehaviour.LuaGC);
            if (StarterPath == null || StarterPath.Equals(""))
            {
                StarterPath = Application.dataPath + "/../Export/GameXmlObj/Default.xml";
                FileCreater(StarterPath, "<GameObject Xml_GameObjectName=\"Main\"></GameObject>");
            }
            AssetsManager.Instance.LoadAsset<GameObjectAsset, GameObject>(StarterPath, ProjectStart);
        }

        private void ProjectStart(GameObject obj)
        {
            Debug.Log("Start Success,StarterName:" + obj.name);
        }

        public static void FileCreater(string filePath,string writeData)
        {
            string dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if (!File.Exists(filePath))
            {
                using (FileStream fs = File.Create(filePath))
                {
                    byte[] data = Encoding.UTF8.GetBytes(writeData);
                    fs.Write(data, 0, data.Length);
                }
            }
        }
    }
}
