using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine.UI;

namespace ChunkGame.Assets
{
    public class AssetsManager
    {
        #region
        private static AssetsManager instance;
        public static AssetsManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AssetsManager();
                }
                return instance;
            }
        }
        private AssetsManager()
        {

        }
        #endregion

        public void LoadAsset<T, Obj>(string url, Action<Obj> callback) where T : class, IAsset<Obj> where Obj : class
        {
            IAsset<Obj> assets = Activator.CreateInstance(typeof(T), url) as IAsset<Obj>;
            assets.Read(callback);
        }


    }
}
