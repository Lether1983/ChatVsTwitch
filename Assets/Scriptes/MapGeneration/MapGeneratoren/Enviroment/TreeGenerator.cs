using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : IMapGenerator
{
    int Mapwidth;
    int Mapheight;
    int count;
    int[,] RandomMap;

    public int[,] Generate()
    {
        SpawnTreesOnMap();
        return RandomMap;
    }

    public void Setup(int width, int height, int[,] Randommap)
    {
        Mapwidth = width;
        Mapheight = height;
        RandomMap = Randommap;
        count = Mapwidth * Mapheight / 8;
    }

    private void SpawnTreesOnMap()
    {
        for (int x = 0; x < count; x++)
        {
            int tempX = UnityEngine.Random.Range(0, Mapwidth);
            int tempY = UnityEngine.Random.Range(0, Mapheight);

            if (RandomMap[tempX, tempY] != 0)
            {
                x--;
            }
            else
            {
                if (tempX < 50 || tempX > 70)
                {
                    RandomMap[tempX, tempY] = 9;
                }
            }
        }
    }

    public void Modify()
    {
        //TODO: Logic to Modify the Generator
    }
}
