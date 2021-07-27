using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChunkGame.Utils
{
    public interface IConfigUtils
    {
        string ToConfig(object obj);
        T FromConfig<T>(string config);
    }
}
