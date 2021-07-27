using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChunkGame.Utils
{
    public interface ITimeTask
    {
        int CreateTask(long time, Action func);
        void StopTask(int taskId);
        void PauseTask(int taskId);
        void ContinueTask(int taskId);
    }
}
