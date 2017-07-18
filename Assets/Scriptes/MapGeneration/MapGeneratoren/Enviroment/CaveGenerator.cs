using UnityEngine;
using System.Collections;
using System;

public class CaveGenerator : IMapGenerator
{
    private int MapWidth;
    private int MapHeight;
    public int[,] RandomMap;
    string Name = "CaveGenerator";

    private int PercentareWalls = 40;

    public int[,] Generate()
    {
        RandomFillMap();
        for (int i = 0; i < 5; i++)
        {
            MakeCave();
        }
        return RandomMap;
    }

    public void MakeCave()
    {
        int[,] temp = new int[MapWidth, MapHeight];
        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                temp[x, y] = PlaceWallLogic(x, y);
            }
        }
        RandomMap = temp;
    }

    private int PlaceWallLogic(int x, int y)
    {
        int numOfWalls = GetAdjacentWalls(x, y, 1, 1);

        if (numOfWalls >= 4 && RandomMap[x, y] == 2) return 2;
        else if (numOfWalls >= 5) return 2;
        else return 1;
    }

    private int GetAdjacentWalls(int x, int y, int p1, int p2)
    {
        int startX = x - p1;
        int startY = y - p2;
        int endX = x + p1;
        int endY = y + p2;

        int wallcounter = 0;

        for (int iX = startX; iX <= endX; iX++)
        {
            for (int iY = startY; iY <= endY; iY++)
            {
                if (!(iX == x && iY == y))
                {
                    if (IsWall(iX, iY))
                    {
                        wallcounter += 1;
                    }
                }
            }
        }
        return wallcounter;
    }

    private bool IsWall(int iX, int iY)
    {
        if (IsOutOfBounds(iX, iY))
        {
            return true;
        }
        if ((RandomMap[iX, iY] & 2) > 0)
        {
            return true;
        }
        if ((RandomMap[iX, iY] & 1) > 0)
        {
            return false;
        }
        return false;
    }

    private bool IsOutOfBounds(int iX, int iY)
    {
        if (iX < 0 || iY < 0)
        {
            return true;
        }
        else if (iX > MapWidth - 1 || iY > MapHeight - 1)
        {
            return true;
        }
        return false;
    }

    public void RandomFillMap()
    {
        RandomMap = new int[MapWidth, MapHeight];
        int MapMiddle = 0;

        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                if (x == 0)
                {
                    RandomMap[x, y] = 2;
                }
                else if (x == MapWidth - 1)
                {
                    RandomMap[x, y] = 2;
                }
                else if (y == 0)
                {
                    RandomMap[x, y] = 2;
                }
                else if (y == MapHeight - 1)
                {
                    RandomMap[x, y] = 2;
                }
                else
                {
                    MapMiddle = (MapHeight / 2);

                    if (y == MapMiddle)
                    {
                        RandomMap[x, y] = 1;
                    }
                    else
                    {
                        RandomMap[x, y] = RandomPercent(PercentareWalls);
                    }
                }
            }
        }
    }

    private int RandomPercent(int PercentareWalls)
    {
        if (UnityEngine.Random.Range(0, 100) <= PercentareWalls)
        {
            return 2;
        }
        return 1;
    }


    public void Setup(int width, int height, int[,] Randommap)
    {
        MapWidth = width;
        MapHeight = height;
        RandomMap = Randommap;
    }

    public void Modify(int newValue)
    {
    }

    public string GetName()
    {
        return Name;
    }
}
