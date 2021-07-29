using System;
using System.Collections.Generic;
using UnityEngine;
using ChunkGame.Lua;
using ChunkGame.Utils;
using System.Collections;
using GameRole;

public class GameMain : MonoBehaviour,ICoroutines
{
    public PlayerPlane plane;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        PathConfig.InitPath(Application.dataPath + "/../Export/");
        InitUtils();
        ConfigAnalysis();
    }

    private void Update()
    {
        LuaBehaviour.LuaGC();
    }
    /// <summary>
    /// 初始化工具
    /// </summary>
    public void InitUtils()
    {
        UtilsManager.ResetCoroutinesUtil(this);
    }
    /// <summary>
    /// 解析配置
    /// </summary>
    public void ConfigAnalysis()
    {
        AssetsLoader.GetInstance().ReadExportAssets(() => {
            //finish
            plane.InitPlane("10001");
        });
    }

    void ICoroutines.StartCoroutine(IEnumerator enumerator)
    {
        StartCoroutine(enumerator);
    }
}
