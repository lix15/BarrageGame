using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChunkGame.Utils
{
    public interface ILog
    {
        void Log(object obj);
        void LogWarning(object obj);
        void LogError(object obj);
        void ThrowException(Exception ex);
    }
}
