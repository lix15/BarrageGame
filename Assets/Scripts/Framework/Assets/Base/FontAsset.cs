using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ChunkGame.Assets
{
    public class FontAsset : IAsset<Font>
    {
        public Font getAsset { get; private set; }

        public string Url { get; private set; }

        public FontAsset(string Path)
        {
            Url = Path;
        }

        public void Read(Action<Font> func)
        {
            func(new Font(Url));
        }
    }
}
