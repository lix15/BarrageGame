using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace ChunkGame.Assets
{
    public class Bundle : IAsset<AssetBundle>
    {
        public string Url { get; private set; }

        public AssetBundle getAsset { get; private set; }

        public Bundle(string Path)
        {
            Url = Path;
        }
        public void Read(Action<AssetBundle> func)
        {
            getAsset = AssetBundle.LoadFromFile(Url);
            func(getAsset);
        }
    }
}
