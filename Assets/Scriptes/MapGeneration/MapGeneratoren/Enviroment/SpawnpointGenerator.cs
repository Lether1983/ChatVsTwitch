using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointGenerator : IMapGenerator
{
    private int width;
    private int height;
    private int[,] Randommap;

    public int[,] Generate()
    {
        PlaceSpawnpoint();
        return Randommap;
    }

    private void PlaceSpawnpoint()
    {
        int tempX = UnityEngine.Random.Range(5, 15);
        int tempY = UnityEngine.Random.Range(0, height);

        if(Randommap[tempX,tempY] != 1)
        {
            PlaceSpawnpoint();
        }
        else
        {
            Randommap[tempX, tempY] = 1 + 4 + 262144;
        }
    }

    public void Modify(int newValue)
    {
        //TODO: Logic to Modify the Generator
    }

    public void Setup(int width, int height, int[,] Randommap)
    {
        this.width = width;
        this.height = height;
        this.Randommap = Randommap;
    }
}
