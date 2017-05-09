using UnityEngine;
using System.Collections;
using System;

public class TrapGenerator : IMapGenerator
{
    int Mapwidth;
    int Mapheight;
    int count;
    int[,] RandomMap;

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
        count = UnityEngine.Random.Range(15, 35);
    }

    private void SpawnTrapsOnMap()
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
                    RandomMap[tempX, tempY] = 11;
                }
            }
        }
    }
}
