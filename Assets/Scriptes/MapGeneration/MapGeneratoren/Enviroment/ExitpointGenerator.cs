using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitpointGenerator : IMapGenerator
{
    private int width;
    private int height;
    private int[,] Randommap;

    public int[,] Generate()
    {
        PlaceExitPoint();
        return Randommap;
    }

    private void PlaceExitPoint()
    {
        int tempX = UnityEngine.Random.Range(5, 15);
        int tempY = UnityEngine.Random.Range(0, height);

        if (Randommap[tempX, tempY] != 1)
        {
            PlaceExitPoint();
        }
        else
        {
            Randommap[tempX, tempY] = 1 + 4 + 524288;
        }
    }

    public void Modify(int newValue)
    {
    }

    public void Setup(int width, int height, int[,] Randommap)
    {
        this.width = width;
        this.height = height;
        this.Randommap = Randommap;
    }
}
