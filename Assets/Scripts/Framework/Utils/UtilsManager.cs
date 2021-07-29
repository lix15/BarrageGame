using ChunkGame.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChunkGame.Utils
{
    public class UtilsManager
    {
        public static ILog Loger { get; private set; } = new Loger();
        public static IConfigUtils ConfigUtil { get; private set; } = new JsonConfigUtil();
        public static ITimeTask TimeTask { get; private set; }

        public static ICoroutines CoroutinesUtil { get; private set; }

        public static void ResetLoger(ILog log)
        {
            if (log == null)
            {
                Loger.LogError("Loger can not be null");
                return;
            }
            Loger = Loger;
        }

        public static void ResetConfigUtil(IConfigUtils configUtil)
        {
            if (configUtil == null)
            {
                Loger.LogWarning("not recommended ConfigUtil be null");
            }
            ConfigUtil = configUtil;
        }
    
        public static void ResetTimeTask(ITimeTask timeTask)
        {
            if (timeTask == null)
            {
                Loger.LogWarning("not recommended timeTask be null");
            }
            TimeTask = timeTask;
        }

        public static void ResetCoroutinesUtil(ICoroutines coroutines)
        {
            if (coroutines == null)
            {
                Loger.LogWarning("not recommended CoroutinesUtil be null");
            }
            CoroutinesUtil = coroutines;
        }
    }
}
