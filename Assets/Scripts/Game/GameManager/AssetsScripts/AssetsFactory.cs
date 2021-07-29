using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;

public class AssetsFactory
{
    public static void GetPlayerObject(string key, Action<GameObject> finish)
    {
        DirectoryInfo planePackage = AssetsLoader.GetInstance().PlayerPackage[key];
        string Des = planePackage.FullName + PathConfig.Player.Des;
        string PlaneConfig = planePackage.FullName + PathConfig.Player.PlaneConfig;

    }
}
