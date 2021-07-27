using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChunkGame.Utils
{
    public interface ICoroutines
    {
        void StartCoroutine(IEnumerator enumerator);
    }
}
