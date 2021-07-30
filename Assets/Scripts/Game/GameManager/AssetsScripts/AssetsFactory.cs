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

    public TextAsset defaultPlaneModel;

    private static AssetsFactory self;
    private void Awake()
    {
        self = this;
    }

    public static void GetPlayerObject(string key, Action<PlayerPackage> finish)
    {
        PlayerPackage package = new PlayerPackage();
        DirectoryInfo planePackage = AssetsLoader.GetInstance().PlayerPackage[key];
        string Des = planePackage.FullName + PathConfig.Player.Des;
        string Plane = planePackage.FullName + PathConfig.Player.PlaneConfig;
        IOTools.FileCreater(Des, self.defaultDes.text);
        IOTools.FileCreater(Plane, self.defaultPlane.text);
        PackageDesConfig desConfig = UtilsManager.ConfigUtil.FromConfig<PackageDesConfig>(File.ReadAllText(Des));
        package.Des = desConfig;
        PlaneConfig planeConfig = UtilsManager.ConfigUtil.FromConfig<PlaneConfig>(File.ReadAllText(Plane));
        package.Config = planeConfig;

        string planeModel = planePackage.FullName + "/" + planeConfig.ModelXml;
        IOTools.FileCreater(planeModel, self.defaultPlaneModel.text);
        GameObjectAsset gObj = new GameObjectAsset(planeModel);
        gObj.Read((planeObj) => {
            planeObj.transform.SetParent(self.PlanePool);
            package.Plane = planeObj.GetComponent<PlaneCore>();
            finish(package);
        });
    }
}
