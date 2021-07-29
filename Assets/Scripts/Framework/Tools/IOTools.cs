using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
                fs.Close();
            }
        }
    }

    public static void Serializable(string savePath, object saveObj)
    {
        FileCreater(savePath, "");
        using (MemoryStream memoryStream = new MemoryStream())
        {
            BinaryFormatter seralizer = new BinaryFormatter();
            seralizer.Serialize(memoryStream, saveObj);
            FileStream fs = new FileStream(savePath, FileMode.Open, FileAccess.Write);
            byte[] buffer = new byte[1024];
            int b = 1;
            while (b > 0)
            {
                b = memoryStream.Read(buffer, 0, buffer.Length);
                fs.Write(buffer, 0, buffer.Length);
            }
            memoryStream.Close();
            fs.Close();
        }
    }

    public static object Deserializable(string filePath)
    {
        using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            MemoryStream m = new MemoryStream();
            byte[] buffer = new byte[1024];
            int b = 1;
            while (b > 0)
            {
                b = file.Read(buffer, 0, buffer.Length);
                m.Write(buffer, 0, buffer.Length);
            }
            BinaryFormatter bf = new BinaryFormatter();
            object data = bf.Deserialize(m);
            file.Close();
            m.Close();
            return data;
        }
    }

}
