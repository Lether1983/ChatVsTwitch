using UnityEngine;
using System.Collections;
using System;

public class LookToMouseCursor : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        float angle = AngleBetweenTwoPoints(-positionOnScreen, -mouseOnScreen);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    private float AngleBetweenTwoPoints(Vector2 positionOnScreen, Vector2 mouseOnScreen)
    {
        return Mathf.Atan2(positionOnScreen.y - mouseOnScreen.y, positionOnScreen.x - mouseOnScreen.x) * Mathf.Rad2Deg;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "ColliderPrefab(Clone)")
        {
            Debug.Log("Penis");
        }
    }
}
