using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTarget : TargetScript
{
    Enemy enemy;
    public void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if(enemy.Health <= 0)
        {
            gManager.CompletTarget = true;
        }
    }
}
