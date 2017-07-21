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
    [SerializeField]
    private GameObject placeholderPrefab;
    [SerializeField]
    private SpawnObject[] spawnObjects = null;
    [SerializeField]
    private GameObject ActivePlayer;

    private Vector2 Startposition;

    public GameObject GetPlayer
    {
        get { return ActivePlayer; }
    }

    public void SpawnObjects(Map levelMap)
    {
        GameObject temp2 = Instantiate(placeholderPrefab, this.transform.position, Quaternion.identity) as GameObject;
        temp2.transform.SetParent(this.gameObject.transform);
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
                                SpriteGetter Temp =Instantiate(spawnObjects[i].Prefab, new Vector2(x, y), Quaternion.identity);
                                Temp.tManager = tManager;
                                Temp.gameObject.transform.SetParent(temp2.transform);
                            }
                            break;
                        }
                    }
                }
            }
        }
    }

    public void RestartPlayerPosition()
    {
        ActivePlayer = Instantiate(Player, Startposition, Quaternion.identity) as GameObject;
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
                        Startposition = new Vector2(x, y);
                        ActivePlayer = Instantiate(Player, Startposition, Quaternion.identity) as GameObject;
                        Instantiate(Camera, new Vector3(x, y,-10), Quaternion.identity);
                    }
                }
            }
        }
    }

    public void SpawnIncomeingFire(int x, int y)
    {
        Instantiate(MorterObject, new Vector2(x, y), Quaternion.identity);
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