using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireWeapon : MonoBehaviour
{
    [SerializeField]
    private GameManager gmanager;
    [SerializeField]
    private float timer;

    private void Start()
    {
        gmanager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (this.gameObject.GetComponent<EnemyControlUnit>().IsInFireDistance )
        {
            if (timer > 1)
            {
                FireWeapon();
                timer = 0;
            }
        }
    }

    public void FireWeapon()
    {
        for (int i = 0; i < gmanager.EnemyBulletPool.Count; i++)
        {
            if (!gmanager.EnemyBulletPool[i].activeInHierarchy)
            {
                gmanager.EnemyBulletPool[i].transform.position = transform.position;
                gmanager.EnemyBulletPool[i].transform.rotation = transform.rotation;
                gmanager.EnemyBulletPool[i].SetActive(true);
                gmanager.EnemyBulletPool[i].GetComponent<Rigidbody2D>().AddForce(gmanager.EnemyBulletPool[i].transform.right * 250);
                break;
            }
        }
    }
}
