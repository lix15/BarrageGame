using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class PlaneConfig
{
    public string Name;
    public string Description;
    public int Hp;
    public int Defense;
    public string ModelXml;
    public string DefaultMachine;
}
[Serializable]
public class MachineGunConfig
{
    public string Name;
    public string Description;
    public int Attack;
    public int BulletClip;
    public string GunXml;
}