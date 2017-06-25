using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarObject : MonoBehaviour
{
    [SerializeField]
    float timer = 0.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            timer = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("InTarget");
        if (timer >= 5)
        {
            collision.gameObject.GetComponentInParent<Player>().getDamage(45);
            Debug.Log(collision.gameObject.GetComponentInParent<Player>().Health);
            Destroy(this.gameObject);
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
            timer = 0;
        }
    }
}
