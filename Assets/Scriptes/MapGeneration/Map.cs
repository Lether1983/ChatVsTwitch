using UnityEngine;
using System.Collections.Generic;

public enum Direction { North, South, West, East }

public class Map 
{
    public int MapWidth;
    public int MapHeight;
    public int[,] RandomMap;

    public List<IMapGenerator> ActiveMap;
    IMapGenerator MapGenerator;
    
    public Map()
    {
        MapWidth = 120;
        MapHeight = 26;
        ActiveMap = new List<IMapGenerator>();
        RandomMap = new int[MapWidth, MapHeight];
    }

    public int Get(Vector2 v)
    {
        return RandomMap[(int)v.x,(int)v.y];
    }

    public int Get(int x, int y)
    {
        return RandomMap[x,y];
    }

    public void AddDecorater(IMapGenerator decorater)
    {
        ActiveMap.Add(decorater);
    }

    public void RemoveDecorater(IMapGenerator decorater)
    {
        ActiveMap.Remove(decorater);
    }

    public void ModifyDecorater(string decorator,int newCount)
    {
        foreach (var item in ActiveMap)
        {
            if(item.GetName() == decorator)
            {
                item.Modify(newCount);
            }
        }
    }
    public void ClearList()
    {
        //TODO: Logic for Clear the Level list
    }

    public void CreateNewMap()
    {
        for (int i = 0; i < ActiveMap.Count; i++)
        {
            MapGenerator = ActiveMap[i];
            MapGenerator.Setup(MapWidth, MapHeight, RandomMap);
            RandomMap = MapGenerator.Generate();
        }
    }
}
