using UnityEngine;
using System.Collections.Generic;

public enum Direction { North, South, West, East }

public class Map 
{
    public int MapWidth;
    public int MapHeight;
    public int[,] RandomMap;

    List<IMapGenerator> ActiveMap;
    IMapGenerator MapGenerator;
    
    public Map()
    {
        MapWidth = 120;
        MapHeight = 26;
        ActiveMap = new List<IMapGenerator>();
        RandomMap = new int[MapWidth, MapHeight];
    }

    public void AddDecorater(IMapGenerator decorater)
    {
        ActiveMap.Add(decorater);
    }

    public void ModifyDecorater(IMapGenerator decorator)
    {
        //TODO: Add Logic for Modify the Decoators
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
