using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueTarget : TargetScript
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<RescueCivilian>().CivilianToAdd = this.gameObject;
        collision.GetComponent<RescueCivilian>().Active = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<RescueCivilian>().CivilianToAdd = null;
        collision.GetComponent<RescueCivilian>().Active = false;
    }

}
