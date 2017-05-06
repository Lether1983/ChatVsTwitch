using UnityEngine;
using System.Collections.Generic;

public enum Direction { North, South, West, East }

public class Map 
{
    public int MapWidth;
    public int MapHeight;
    public int[,] RandomMap;

    List<IMapGenerator> MapDecorators;
    IMapGenerator MapGenerator;
    
    public Map()
    {
        MapWidth = 120;
        MapHeight = 26;
        MapDecorators = new List<IMapGenerator>();
        RandomMap = new int[MapWidth, MapHeight];
    }

    public void AddDecorater(IMapGenerator decorater)
    {
        MapDecorators.Add(decorater);
    }

    public void ModifyDecorater(IMapGenerator decorator)
    {

    }

    public void CreateNewMap()
    {
        for (int i = 0; i < MapDecorators.Count; i++)
        {
            MapGenerator = MapDecorators[i];
            MapGenerator.Setup(MapWidth, MapHeight, RandomMap);
            RandomMap = MapGenerator.Generate();
        }
    }
}
