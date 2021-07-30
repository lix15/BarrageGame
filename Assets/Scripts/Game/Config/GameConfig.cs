using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class GameConfig
{
    public string Version;

    public int Plane_Num_X = 2;
    public int Plane_Num_Y = 2;
    
    public int Plane_Group_X_PX = 50;
    public int Plane_Group_Y_PX = 50;

    public int Bullet_Size_X_PX;
    public int Bullet_Size_Y_PX;

    public int Enemy_Num_X;
    public int Enemy_Num_Y;
    public int Enemy_Group_X_PX;
    public int Enemy_Group_Y_PX;
}
