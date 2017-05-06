using UnityEngine;
using System.Collections;

public class PlayerControlUnit : MonoBehaviour
{
    Vector2 Movedirection;
    public float speed;

    void Start()
    {
        Movedirection = Vector2.zero;
    }
    void Update()
    {
        Movedirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        transform.Translate(Movedirection*speed*Time.deltaTime);
    }
}
