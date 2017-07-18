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
    string Name = "TreeGenerator";

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
        count = (Mapwidth - 20) * Mapheight / 8;
    }

    private void SpawnTreesOnMap()
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
                if (tempX < 50 || tempX > 70)
                {
                    RandomMap[tempX, tempY] = 1 + 4 + 32768;
                }
            }
        }
    }

    public void Modify(int newValue)
    {
        //TODO: Logic to Modify the Generator
    }

    public string GetName()
    {
        return Name;
    }
}
