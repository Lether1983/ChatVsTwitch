using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushGenerator : IMapGenerator
{
    int Mapwidth;
    int Mapheight;
    int count;
    int[,] RandomMap;
    string Name = "BushGenerator";

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
        count = Mapwidth * Mapheight / 8;
    }

    private void SpawnBushesOnMap()
    {
        for (int x = 0; x < count; x++)
        {
            int tempX = UnityEngine.Random.Range(0, Mapwidth);
            int tempY = UnityEngine.Random.Range(0, Mapheight);
            if (RandomMap[tempX, tempY] != 1)
            {
                x--;
            }
            else
            {
                RandomMap[tempX, tempY] = 1 + 4 + 16384;
            }
        }
    }

    public void Modify(int count)
    {
    }

    public string GetName()
    {
        return Name;
    }
}
