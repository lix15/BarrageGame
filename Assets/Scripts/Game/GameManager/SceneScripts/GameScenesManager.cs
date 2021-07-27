using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameScenesManager
{
    

    private static Stack<string> _ScenesStack = new Stack<string>();
    private static SceneLoader _Loader;
    private static string currentScene;

    public static void Init(SceneLoader loader)
    {
        _Loader = loader;
    }

    public static void ToScene(string sceneName)
    {
        currentScene = sceneName;
        _ScenesStack.Push(currentScene);
        _Loader?.Open(sceneName);
    }

    public static void Back()
    {
        _ScenesStack.Pop();
        if (_ScenesStack.Count == 0)
        {
            //没有场景
            return;
        }
        currentScene = _ScenesStack.Peek();
        ToScene(currentScene);
    }
}
