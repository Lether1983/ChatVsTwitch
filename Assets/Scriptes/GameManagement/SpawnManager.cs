using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private TileManager tManager;

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

    public int Key
    {
        get { return key; }
    }

}