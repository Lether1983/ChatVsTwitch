using UnityEngine;
using System.Collections;
using System;

public class MineGenerator : IMapGenerator
{
    int Mapwidth;
    int Mapheight;
    int[,] RandomMap;
    int count;

    public int[,] Generate()
    {
        SpawnMines();
        return RandomMap;
    }

    public void Setup(int width, int height, int[,] Randommap)
    {
        Mapwidth = width;
        Mapheight = height;
        RandomMap = Randommap;
        count = UnityEngine.Random.Range(15, 35);
    }

    private void SpawnMines()
    {
        int temp = 0;
        for (int i = 0; i < count; i++)
        {
            int tempX = UnityEngine.Random.Range(15, 55);
            int tempY = UnityEngine.Random.Range(5, Mapheight-5);
            if(RandomMap[tempX,tempY] != 0)
            {
                i--;
            }
            else
            {
                if(temp <= count)
                {
                    RandomMap[tempX, tempY] = 6;
                    temp++;
                }
            }
        }
    }
}
