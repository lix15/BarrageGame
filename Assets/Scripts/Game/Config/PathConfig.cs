using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class PathConfig
{
    public static string LOCAL_PATH;
    public static string PLAYER_PLANE;
    public static string ENEMY_PLANE;
    public static string PROP;
    public static string CONFIG;

    public static void InitPath(string localPath)
    {
        LOCAL_PATH = localPath;
        PLAYER_PLANE = LOCAL_PATH + "Player/";
        ENEMY_PLANE = LOCAL_PATH + "Enemy/";
        PROP = LOCAL_PATH + "Prop/";
        CONFIG = LOCAL_PATH + "config/";
    }


    public static class Game
    {
        public const string DefalutConfig = "/GameDefaultConfig.json";
    }

    public static class Player
    {
        public const string Des = "/Description.json";
        public const string PlaneXml = "/PlaneCore.xml";
        public const string PlaneConfig = "/Plane.json";
        public const string DefaultMachineGunXml = "/DefaultMachineGun.xml";
        public const string DefaultMachineGunConfig = "/MachineGun.json";
    }

    public static class Prop
    {
        public const string Des = "/Description.json";
        public const string PropXml = "/PropCore.Xml";
        public const string PropConfig = "/Prop.json";
    }

    public static class Enemy
    {
        public const string Des = "/Description.json";
        public const string EnmeyConfig = "/Enemy.json";
    }
}
