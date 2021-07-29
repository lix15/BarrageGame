using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class PathConfig
{
    public static string LOCAL_PATH = Application.dataPath + "/../Export/";

    public static string PLAYER_PLANE = LOCAL_PATH + "Player/";
    public static string ENEMY_PLANE = LOCAL_PATH + "Enmey/";
    public static string PROP = LOCAL_PATH + "Prop/";
    public static string CONFIG = LOCAL_PATH + "config/";

    public static class Player
    {
        public const string Des = "";
        public const string PlaneXml = "";
        public const string PlaneConfig = "";
        public const string DefaultMachineGunXml = "";
        public const string DefaultMachineGunConfig = "";
    }

    public static class Prop
    {
        public const string Des = "";
        public const string PropXml = "";
        public const string PropConfig = "";
    }

    public static class Enemy
    {
        public const string Des = "";
        public const string EnmeyConfig = "";
    }
}
