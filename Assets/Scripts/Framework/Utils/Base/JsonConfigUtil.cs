using UnityEngine;

namespace ChunkGame.Utils
{
    public class JsonConfigUtil : IConfigUtils
    {
        public T FromConfig<T>(string config)
        {
            return JsonUtility.FromJson<T>(config);
        }

        public string ToConfig(object obj)
        {
            return JsonUtility.ToJson(obj);
        }
    }
}
