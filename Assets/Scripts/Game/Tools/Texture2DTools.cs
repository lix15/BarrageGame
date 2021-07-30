using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ChunkGame.Assets;

public class Texture2DTools
{
    /// <summary>
    /// 图集加载
    /// </summary>
    /// <param name="Url">图片路径</param>
    /// <param name="wAh">图片数量</param>
    /// <param name="groupSize">图集尺寸</param>
    /// <param name="callBack">加载成功回调</param>
    public static void GetTextureGroup(string Url, Vector2Int wAh, Vector2Int groupSize, Action<Sprite[]> callBack)
    {
        Sprite[] group = new Sprite[wAh.x * wAh.y];
        Texture2DAsset texture = new Texture2DAsset(Url, groupSize);
        int item_w = groupSize.x / wAh.x;
        int item_h = groupSize.y / wAh.y;
        int index = 0;
        texture.Read((texture) =>
        {
            for (int j = wAh.y - 1; j >= 0; j--)
            {
                for (int i = 0; i < wAh.x ; i++)
                {
                    Texture2D item = new Texture2D(item_w, item_h);
                    DepackTexture(i, j, item, texture);
                    item.Apply();
                    group[index] = Sprite.Create(item, new Rect(0, 0, item_w, item_h), new Vector2(0.5f, 0.5f));
                    index++;
                }
            }
            callBack(group);
        });
    }
    /// <summary>
    /// 图片切割
    /// </summary>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <param name="texture"></param>
    /// <param name="mainT"></param>
    private static void DepackTexture(int i,int j, Texture2D texture,Texture2D mainT)
    {
        int cur_x = texture.width * i;
        int cur_y = texture.height * j;
        for (int m = cur_y; m < cur_y + texture.height; ++m)
        {
            for (int n = cur_x; n < cur_x + texture.width; ++n)
            {
                texture.SetPixel(n - cur_x, m - cur_y, mainT.GetPixel(n, m));
            }
        }
    }
}
