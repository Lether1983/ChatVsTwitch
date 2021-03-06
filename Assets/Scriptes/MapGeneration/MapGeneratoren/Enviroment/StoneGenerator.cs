﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGenerator : IMapGenerator
{
    int Mapwidth;
    int Mapheight;
    int count;
    int[,] RandomMap;
    string Name = "StoneGenerator";
    public int[,] Generate()
    {
        SpawnStonesOnMap();
        return RandomMap;
    }

    public void Setup(int width, int height, int[,] Randommap)
    {
        Mapwidth = width;
        Mapheight = height;
        RandomMap = Randommap;
        count = Mapwidth * Mapheight / 16;
    }


    private void SpawnStonesOnMap()
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
                RandomMap[tempX, tempY] = 1 + 4 + 65536;
            }
        }
    }

    public void Modify(int newValue)
    {
    }

    public string GetName()
    {
        return Name;
    }
}
