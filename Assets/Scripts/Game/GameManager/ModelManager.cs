using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ModelManager:MonoBehaviour
{
    public static ModelManager Instance;
    public GameCoroutines _GameCoroutines;
    public SceneLoader _SceneLoader;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
