using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueTarget : TargetScript
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponentInParent<RescueCivilian>().CivilianToAdd = this.gameObject;
        collision.GetComponentInParent<RescueCivilian>().Active = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponentInParent<RescueCivilian>().Active = false;
    }

}
