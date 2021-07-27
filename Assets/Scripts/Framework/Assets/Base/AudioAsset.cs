using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using ChunkGame.Utils;
using System.IO;

namespace ChunkGame.Assets
{
    public class AudioAsset : IAsset<AudioClip>
    {
        public string Url { get; private set; }

        public AudioClip getAsset { get; private set; }

        public AudioAsset(string Path)
        {
            Url = Path;
        }
        public void Read(Action<AudioClip> func)
        {
            if (UtilsManager.CoroutinesUtil != null)
            {
                UtilsManager.CoroutinesUtil.StartCoroutine(LoadAudio(func));
            }
            else
            {
                UtilsManager.Loger.LogError("Audio Load Error;Coroutines is Null");
            }
        }

        private IEnumerator LoadAudio(Action<AudioClip> func)
        {
            //string ext = new FileInfo(Url).Extension;
            
            UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(Url, AudioType.MPEG);
            yield return uwr.SendWebRequest();
            if (uwr.error != null)
            {
                UtilsManager.Loger.LogError(uwr.error);
                func(null);
                yield break;
            }
            getAsset = DownloadHandlerAudioClip.GetContent(uwr);
            func(getAsset);
        }
    }
}
