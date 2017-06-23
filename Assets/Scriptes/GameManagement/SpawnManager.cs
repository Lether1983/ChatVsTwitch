using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private TileManager tManager;

    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject Camera;
    [SerializeField]
    private GameObject MorterObject;

    private GameObject ActivePlayer;

    public GameObject GetPlayer
    {
        get { return ActivePlayer; }
    }


    [SerializeField]
    private SpawnObject[] spawnObjects = null;

    public void SpawnObjects(Map levelMap)
    {
        for (int x = 0; x < levelMap.MapWidth; x++)
        {
            for (int y = 0; y < levelMap.MapHeight; y++)
            {
                if ((levelMap.Get(x, y) & 4) > 0)
                {
                    var entity = levelMap.Get(x, y) >> 10;

                    for (int i = 0; i < spawnObjects.Length; i++)
                    {
                        if (spawnObjects[i].Key == entity)
                        {
                            if (spawnObjects[i].Name == "ExitPoint") continue;
                            if (spawnObjects[i].Prefab)
                            {
                                Instantiate(spawnObjects[i].Prefab, new Vector2(x, y), Quaternion.identity).tManager = tManager;
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
    public void SpawnPlayer(Map levelmap)
    {
        for (int x = 0; x < levelmap.MapWidth; x++)
        {
            for (int y = 0; y < levelmap.MapHeight; y++)
            {
                if ((levelmap.Get(x, y) & 4) > 0)
                {
                    var entity = levelmap.Get(x, y) >> 10;
                    if (entity == 256)
                    {
                        ActivePlayer = Instantiate(Player, new Vector2(x, y), Quaternion.identity) as GameObject;
                        Instantiate(Camera, new Vector3(x, y,-10), Quaternion.identity);
                    }
                }
            }
        }
    }
    public void SpawnIncomeingFire(int x, int y)
    {
        //TODO: Uncommend
        //Instantiate(MorterObject, new Vector2(x, y), Quaternion.identity);
    }

    public void SpawnExitPoint(Map levelmap)
    {
        for (int x = 0; x < levelmap.MapWidth; x++)
        {
            for (int y = 0; y < levelmap.MapHeight; y++)
            {
                if ((levelmap.Get(x, y) & 4) > 0)
                {
                    var entity = levelmap.Get(x, y) >> 10;
                    if (entity == 512)
                    {
                        foreach (var item in spawnObjects)
                        {
                            if(item.Name == "ExitPoint")
                            {
                                Instantiate(item.Prefab, new Vector2(x, y), Quaternion.identity).tManager = tManager;
                            }
                        }
                    }
                }
            }
        }
    }
}


[Serializable]
public struct SpawnObject
{
    [SerializeField]
    private string name;
    [SerializeField]
    private int key;
    [SerializeField]
    private SpriteGetter prefab;

    public SpriteGetter Prefab
    {
        get { return prefab; }
    }
    public string Name
    {
        get { return name; }
    }
    public int Key
    {
        get { return key; }
    }

}