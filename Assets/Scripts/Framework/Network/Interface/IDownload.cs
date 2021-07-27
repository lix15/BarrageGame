using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChunkGame.Net
{
    public interface IDownload
    {
        void Download(string url, string savePath, Action<FileInfo> callback);
    }
}
