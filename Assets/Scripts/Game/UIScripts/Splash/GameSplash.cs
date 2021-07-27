using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSplash:MonoBehaviour
{

    public void ToMain()
    {
        GameScenesManager.ToScene(SceneConfig.MAIN_MENU_SCENE_NAME);
    }
}
