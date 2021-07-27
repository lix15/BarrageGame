using System;
using System.IO;
using System.Net;
using System.Threading;

namespace ChunkGame.Net
{
    public class DownloadUtil : IDownload
    {
        public void Download(string url, string savePath, Action<FileInfo> callback)
        {
            new Thread(() => {
                try
                {
                    string dir = Path.GetDirectoryName(savePath);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(url);
                    using (Stream stream = hwr.GetResponse().GetResponseStream())
                    {
                        using (FileStream fs = File.Create(savePath))
                        {
                            byte[] buffer = new byte[1024 * 100];
                            int n = 1;
                            while (n > 0)
                            {
                                n = stream.Read(buffer, 0, buffer.Length);
                                fs.Write(buffer, 0, n);
                            }
                        }
                    }
                    callback(new FileInfo(savePath));
                }
                catch (Exception e)
                {
                    callback(null);
                }

            }).Start();
        }
    }
}
