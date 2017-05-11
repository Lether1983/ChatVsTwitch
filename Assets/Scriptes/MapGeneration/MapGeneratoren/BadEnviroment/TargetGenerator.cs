using UnityEngine;
using System.Collections;
using System;

public class TargetGenerator : IMapGenerator
{
    int Mapwidth;
    int MapHeight;
    int count;
    int[,] RandomMap;


    public int[,] Generate()
    {
        PlaceTaget();
        return RandomMap;
    }

    public void Setup(int width, int height, int[,] Randommap)
    {
        Mapwidth = width;
        MapHeight = height;
        RandomMap = Randommap;
        count = 1;
    }

    private void PlaceTaget()
    {
        for (int i = 0; i < count; i++)
        {
            int tempX = UnityEngine.Random.Range(Mapwidth - 20, Mapwidth);
            int tempY = UnityEngine.Random.Range(0, MapHeight);

            if (RandomMap[tempX, tempY] != 0)
            {
                PlaceTaget();
            }
            else
            {
                RandomMap[tempX, tempY] = 7;
            }
        }
    }

    public void Modify()
    {
        //TODO: Logic to Modify the Generator
    }
}
