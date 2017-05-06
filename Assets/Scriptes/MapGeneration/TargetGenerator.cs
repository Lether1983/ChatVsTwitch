using UnityEngine;
using System.Collections;
using System;

public class TargetGenerator : IMapGenerator
{
    int Mapwidth;
    int MapHeight;
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
    }

    private void PlaceTaget()
    {
        int tempX = UnityEngine.Random.Range(Mapwidth - 20, Mapwidth);
        int tempY = UnityEngine.Random.Range(0, MapHeight);

        if(RandomMap[tempX,tempY] != 0)
        {
            PlaceTaget();
        } 
        else
        {
            RandomMap[tempX, tempY] = 7;
        }
    }
}
