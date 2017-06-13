using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    [SerializeField]
    private int damage = 25;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Player>().getDamage(damage);
        Destroy(this.gameObject);
    }
}
