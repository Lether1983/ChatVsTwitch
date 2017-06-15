using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireWeapon : MonoBehaviour
{
    public GameObject SpritePrefab;
    public List<GameObject> BulletPool;

    void Start()
    {
        BulletPool = new List<GameObject>();

        for (int i = 0; i < 20; i++)
        {
            GameObject Temp = Instantiate(SpritePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            Temp.SetActive(false);
            BulletPool.Add(Temp);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < BulletPool.Count; i++)
            {
                if (!BulletPool[i].activeInHierarchy)
                {
                    BulletPool[i].transform.position = transform.position;
                    BulletPool[i].transform.rotation = transform.rotation;
                    BulletPool[i].SetActive(true);
                    break;
                }
            }
        }
    }
}