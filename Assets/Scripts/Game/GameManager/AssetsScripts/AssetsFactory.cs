using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;
using ChunkGame.Utils;
using ChunkGame.Assets;
using GameRole;

public class AssetsFactory : MonoBehaviour
{
    public Transform PlanePool;

    public TextAsset defaultDes;
    public TextAsset defaultPlane;

    private static AssetsFactory Self;
    private void Awake()
    {
        Self = this;
    }

    public static void GetPlayerObject(string key, Action<PlayerPackage> finish)
    {
        PlayerPackage package = new PlayerPackage();
        DirectoryInfo planePackage = AssetsLoader.GetInstance().PlayerPackage[key];
        string Des = planePackage.FullName + PathConfig.Player.Des;
        string Plane = planePackage.FullName + PathConfig.Player.PlaneConfig;
        IOTools.FileCreater(Des, Self.defaultDes.text);
        IOTools.FileCreater(Plane, Self.defaultPlane.text);
        PackageDesConfig desConfig = UtilsManager.ConfigUtil.FromConfig<PackageDesConfig>(File.ReadAllText(Des));
        package.Des = desConfig;
        PlaneConfig planeConfig = UtilsManager.ConfigUtil.FromConfig<PlaneConfig>(File.ReadAllText(Plane));
        package.Config = planeConfig;

        GameObjectAsset gObj = new GameObjectAsset(planeConfig.ModelXml);
        gObj.Read((planeObj) => {
            planeObj.transform.SetParent(Self.PlanePool);
            planeObj.SetActive(false);
            package.Plane = planeObj.GetComponent<PlaneCore>();
            finish(package);
        });
    }
}
