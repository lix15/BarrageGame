using System;
using System.Collections.Generic;
using UnityEngine;
using ChunkGame.Lua;
using ChunkGame.Utils;
using System.Collections;

public class GameMain : MonoBehaviour,ICoroutines
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        InitUtils();
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

    }

    public void GameObjectCreateFromXml(string path, Action<GameObject> finish)
    {

    }

    void ICoroutines.StartCoroutine(IEnumerator enumerator)
    {
        StartCoroutine(enumerator);
    }
}
