using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTarget : TargetScript
{
    private bool ActivateTimer;
    private float timer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActivateTimer = true;
    }

    private void Update()
    {
        if(ActivateTimer)
        {
            timer += Time.deltaTime;
            if (timer >= 5)
            {
                gManager.CompletTarget = true;
                Destroy(this.gameObject);
            }
        }
    }
}
