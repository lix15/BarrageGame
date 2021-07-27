using ChunkGame.Assets;
using ChunkGame.Attribute;
using System;
using System.Collections;
using UnityEngine;
using XLua;

namespace ChunkGame.Lua
{
    public class LuaBehaviour:MonoBehaviour
    {
        static LuaEnv luaEnv = new LuaEnv();
        static float lastGCTime = 0;
        const float GCInterval = 1;

        [ToXml]
        public string LuaPath;

        protected LuaTable scriptEnv;
        protected virtual void Start()
        {
            scriptEnv = luaEnv.NewTable();
            LuaPath = LuaPath.Replace("{$}",Application.dataPath + "/../Export/Lua/");
            if (LuaPath.Equals(""))
            {
                Debug.LogError("Lua路径为空");
                return;
            }
            MainStarter.FileCreater(LuaPath, "--这个lua脚本不存在，所以我帮你创建了。(*･ω< ) \n" + "function start()\nend");
            AssetsManager.Instance.LoadAsset<StringAsset, string>(LuaPath, InitScript);
        }

        protected virtual void InitScript(string script)
        {            
            // 为每个脚本设置一个独立的环境，可一定程度上防止脚本间全局变量、函数冲突
            LuaTable meta = luaEnv.NewTable();
            meta.Set("__index", luaEnv.Global);
            scriptEnv.SetMetaTable(meta);
            meta.Dispose();

            scriptEnv.Set("self", this);
            luaEnv.DoString(script, env:scriptEnv);
            Action start;
            scriptEnv.Get("start", out start);
            start?.Invoke();
        }

        public static void LuaGC()
        {
            if (Time.time - lastGCTime > GCInterval)
            {
                luaEnv.Tick();
                lastGCTime = Time.time;
            }
        }
    }
}
