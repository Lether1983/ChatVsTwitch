using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushGenerator : IMapGenerator
{
    int Mapwidth;
    int Mapheight;
    int[,] RandomMap;

    public int[,] Generate()
    {
        SpawnBushesOnMap();
        return RandomMap;
    }

    public void Setup(int width, int height, int[,] Randommap)
    {
        Mapwidth = width;
        Mapheight = height;
        RandomMap = Randommap;
    }

    private void SpawnBushesOnMap()
    {

        int tempX = UnityEngine.Random.Range(0, Mapwidth);
        int tempY = UnityEngine.Random.Range(0, Mapheight);
        for (int x = 0; x < Mapwidth; x++)
        {
            if (tempX < 50 || tempX > 70)
            {
                if (RandomMap[tempX, tempY] != 0)
                {
                       
                }
            }
        }
    }
}
