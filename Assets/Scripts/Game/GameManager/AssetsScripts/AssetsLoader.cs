using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ChunkGame.Utils;

public class AssetsLoader : Singleton<AssetsLoader>
{
    private AssetsLoader() { }

    public Dictionary<string, DirectoryInfo> PlayerPackage = new Dictionary<string, DirectoryInfo>();
    public Dictionary<string, DirectoryInfo> EnemyPackage = new Dictionary<string, DirectoryInfo>();
    public Dictionary<string, DirectoryInfo> PropPackage = new Dictionary<string, DirectoryInfo>();
    public GameConfig _GameConfig = new GameConfig();

    public void ReadExportAssets(Action Finish)
    {
        DirectoryInspect(PathConfig.PLAYER_PLANE);
        DirectoryInspect(PathConfig.ENEMY_PLANE);
        DirectoryInspect(PathConfig.PROP);
        DirectoryInspect(PathConfig.CONFIG);
        Read(Finish);
    }

    public async void Read(Action Finish)
    {
        await Task.Run(() => {
            ReadConfig();
            ReadDirectory(PlayerPackage, PathConfig.PLAYER_PLANE);
            ReadDirectory(EnemyPackage, PathConfig.ENEMY_PLANE);
            ReadDirectory(PropPackage, PathConfig.PROP);
        });
        Finish();
    }

    private void ReadDirectory(Dictionary<string, DirectoryInfo> dic,string path)
    {
        DirectoryInfo playerPackage = new DirectoryInfo(path);
        DirectoryInfo[] dirs = playerPackage.GetDirectories();
        for (int i = 0; i < dirs.Length; i++)
        {
            dic[dirs[i].Name] = dirs[i];
        }
    }

    private void ReadConfig()
    {
        string path = PathConfig.CONFIG + PathConfig.Game.DefalutConfig;
        if (!File.Exists(path))
        {
            File.WriteAllText(path, UtilsManager.ConfigUtil.ToConfig(_GameConfig));
            return;
        }
        _GameConfig = UtilsManager.ConfigUtil.FromConfig<GameConfig>(File.ReadAllText(path));
    }

    /// <summary>
    /// 文件夹检查
    /// </summary>
    /// <param name="path"></param>
    private void DirectoryInspect(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}
