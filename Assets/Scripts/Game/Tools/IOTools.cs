using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class IOTools
{

    public static void FileCreater(string filePath, string writeData)
    {
        string dir = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        if (!File.Exists(filePath))
        {
            using (FileStream fs = File.Create(filePath))
            {
                byte[] data = Encoding.UTF8.GetBytes(writeData);
                fs.Write(data, 0, data.Length);
            }
        }
    }
}
