using UnityEngine;
using System.Collections;

public class PlayerControlUnit : MonoBehaviour
{
    Vector2 Movedirection;
    [SerializeField]
    Rigidbody2D controller;
    public float speed;

    void Start()
    {
        Movedirection = Vector2.zero;
    }
    void FixedUpdate()
    {
        Movedirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        controller.velocity = Movedirection*speed;
    }
}
