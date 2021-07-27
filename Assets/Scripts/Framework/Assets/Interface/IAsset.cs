using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChunkGame.Assets
{
    public interface IAsset<T>
    {
        T getAsset { get; }
        string Url { get; }
        void Read(Action<T> func);
    }
}
