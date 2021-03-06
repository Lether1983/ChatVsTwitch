﻿using UnityEngine;
using System.Collections;
using System;

public class TrapGenerator : IMapGenerator
{
    int Mapwidth;
    int Mapheight;
    int count;
    int[,] RandomMap;
    string Name = "TrapGenerator";
    public int[,] Generate()
    {
        SpawnTrapsOnMap();
        return RandomMap;
    }

    public void Setup(int width, int height, int[,] Randommap)
    {
        Mapwidth = width;
        Mapheight = height;
        RandomMap = Randommap;
        count = 20;
    }

    private void SpawnTrapsOnMap()
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
                    RandomMap[tempX, tempY] = 1 + 4 + 131072;
                }
            }
        }
    }

    public void Modify(int newValue)
    {
        count += newValue;
        if (count <= 20)
        {
            count = 20;
        }
        else if (count >= 50)
        {
            count = 50;
        }
    }

    public string GetName()
    {
        return Name;
    }
}
