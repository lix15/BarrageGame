using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader:MonoBehaviour
{
    private void Awake()
    {
        GameScenesManager.Init(this);
    }

    public void Open(string name)
    {
        StartCoroutine(LoadScene(name));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        while (!scene.isDone)
        {
            if (scene.progress < 0.9f)
            {

            }
            else
            {
                scene.allowSceneActivation = true;
            }
        }

        yield break;
    }
}
