﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    [SerializeField]
    private int damage = 70;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Player>().getDamage(damage);
        Destroy(this.gameObject);
    }
}
