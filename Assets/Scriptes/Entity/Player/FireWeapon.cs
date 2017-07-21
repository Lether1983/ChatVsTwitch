using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireWeapon : MonoBehaviour
{
    public GameObject SpritePrefab;

    private GameManager gManager;

    void Start()
    {
        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gManager.BulletPool.Clear();
        for (int i = 0; i < 20; i++)
        {
            GameObject Temp = Instantiate(SpritePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            Temp.SetActive(false);
            gManager.BulletPool.Add(Temp);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < gManager.BulletPool.Count; i++)
            {
                if (!gManager.BulletPool[i].activeInHierarchy)
                {
                    gManager.BulletPool[i].transform.position = transform.position;
                    gManager.BulletPool[i].transform.rotation = transform.rotation;
                    gManager.BulletPool[i].SetActive(true);
                    gManager.BulletPool[i].GetComponent<Rigidbody2D>().AddForce(gManager.BulletPool[i].transform.right * 250);
                    //BulletPool[i].GetComponent<BulletScript>().Damage = this.gameObject.GetComponent<Player>().getFirePower();
                    break;
                }
            }
        }
    }
}