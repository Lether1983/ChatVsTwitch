using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    float Timer = 0;
    public int Damage;

    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().getDamage(Damage);
            this.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag != "Enemy")
        {
            this.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        Timer += Time.deltaTime;

        if (Timer > 3)
        {
            this.gameObject.SetActive(false);
            Timer = 0;
        }

    }
}
