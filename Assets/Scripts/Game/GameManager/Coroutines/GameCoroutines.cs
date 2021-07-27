using System;
using System.Collections.Generic;
using UnityEngine;
using ChunkGame.Utils;
using System.Collections;

public class GameCoroutines : MonoBehaviour, ICoroutines
{
    private void Awake()
    {
        UtilsManager.ResetCoroutinesUtil(this);
    }
    void ICoroutines.StartCoroutine(IEnumerator enumerator)
    {
        StartCoroutine(enumerator);
    }
}
