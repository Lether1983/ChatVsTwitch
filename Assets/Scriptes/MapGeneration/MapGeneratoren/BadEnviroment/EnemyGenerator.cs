﻿using UnityEngine;
using System.Collections;
using System;

public class EnemyGenerator : IMapGenerator
{
    int Mapwidth;
    int Mapheight;
    int[,] RandomMap;
    int count;
    string Name = "EnemyGenerator";
    public int[,] Generate()
    {
        SpawnEnemys();
        return RandomMap;
    }

    public void Setup(int width, int height, int[,] Randommap)
    {
        Mapwidth = width;
        Mapheight = height;
        RandomMap = Randommap;
        count = 20;
    }

    private void SpawnEnemys()
    {
        int temp = 0;
        for (int i = 0; i < count; i++)
        {
            int tempX = UnityEngine.Random.Range(15, Mapwidth - 20);
            int tempY = UnityEngine.Random.Range(0, Mapheight);
            if (RandomMap[tempX, tempY] != 1)
            {
                i--;
            }
            else
            {
                if (temp <= count)
                {
                    RandomMap[tempX, tempY] = 1 + 4 + 2048;
                    temp++;
                }
            }
        }
    }

    public void Modify(int newValue)
    {
        count += newValue;
        if(count <= 20)
        {
            count = 20;
        }
        else if(count >= 50)
        {
            count = 50;
        }
    }


    public string GetName()
    {
        return Name;
    }
}
