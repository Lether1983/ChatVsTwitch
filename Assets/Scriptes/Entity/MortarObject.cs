using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarObject : MonoBehaviour
{
    [SerializeField]
    float timer = 0.0f;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    private bool isInArea;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player = collision.gameObject;
            isInArea = true;
        }
    }

    private void Update()
    {
        if (timer <= 5)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (Player != null)
            {
                if (this.gameObject.GetComponent<Collider2D>().IsTouching(Player.GetComponentInChildren<Collider2D>()) && isInArea)
                {
                    Player.GetComponentInParent<Player>().getDamage(45);
                    isInArea = false;
                    Destroy(this.gameObject);
                }
            }
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isInArea = false;
        }
    }
}
