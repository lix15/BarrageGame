using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChunkGame.Assets
{
    public class StringAsset : IAsset<string>
    {
        public string Url { get; private set; }

        public string getAsset { get; private set; }

        public StringAsset(string Path)
        {
            Url = Path;
        }

        public StringAsset()
        {
            Url = "";
        }
        public void Read(Action<string> func)
        {
            if (Url == null || Url.Equals(""))
            {
                func("");
                return;
            }
            FileCreate(Url);
            getAsset = File.ReadAllText(Url);
            func(getAsset);
        }

        protected void FileCreate(string path)
        {
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }
    }
}
