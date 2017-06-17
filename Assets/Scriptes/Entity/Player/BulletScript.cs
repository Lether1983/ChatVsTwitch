using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    float Timer = 0;
    public int Damage;

    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().getDamage(Damage);
            this.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag != "Player")
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
