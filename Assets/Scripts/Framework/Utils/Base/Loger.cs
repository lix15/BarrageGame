using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ChunkGame.Utils
{
    public class Loger : ILog
    {
        public void Log(object obj)
        {
            Debug.Log(obj);
        }

        public void LogError(object obj)
        {
            Debug.LogError(obj);
        }

        public void LogWarning(object obj)
        {
            Debug.LogWarning(obj);
        }

        public void ThrowException(Exception ex)
        {
            throw ex;
        }
    }
}
