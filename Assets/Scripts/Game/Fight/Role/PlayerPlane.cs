using ChunkGame.Lua;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameRole
{
    [RequireComponent(typeof(Controller))]
    public class PlayerPlane : MonoBehaviour
    {
        public PlaneCore _Core;

        private PlayerPackage _Package;
        
        public void InitPlane(string key)
        {
            AssetsFactory.GetPlayerObject(key, GotPlane);
        }

        public void GotPlane(PlayerPackage package)
        {
            _Package = package;
            _Core = package.Plane;
            _Core.transform.SetParent(transform);
        }
    }
}
