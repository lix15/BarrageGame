using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ChunkGame.Assets
{
    public class Texture2DAsset : IAsset<Texture2D>
    {
        public string Url { get; private set; }

        public Texture2D getAsset { get; private set; }

        public Texture2DAsset(string url)
        {
            Url = url;
        }

        public Texture2DAsset(string url,Vector2Int size)
        {
            Url = url;
            this.size = size;
        }

        public Vector2Int size { get; set; } = new Vector2Int(0, 0);

        public void Read(Action<Texture2D> func)
        {
            if (!File.Exists(Url))
            {
                func(null);
                return;
            }
            byte[] data = File.ReadAllBytes(Url);
            getAsset = new Texture2D(size.x, size.y);
            getAsset.LoadImage(data);
        }
    }
}
