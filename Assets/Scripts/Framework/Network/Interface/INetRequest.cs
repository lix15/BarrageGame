using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChunkGame.Net
{
    public interface INetRequest
    {
        void Post(string data, Action<string> callback);
        void Get(string data, Action<string> callback);
    }
}
