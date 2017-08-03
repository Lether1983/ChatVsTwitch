using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlUnit : MonoBehaviour
{
    [SerializeField]
    private GameManager gmanager;
    [SerializeField]
    Rigidbody2D contoller;
    [SerializeField]
    Vector2 Movedirection;
    [SerializeField]
    float Speed;
    [SerializeField]
    float turnSpeed;

    private GameObject target;
    
    private void Start()
    {
        Movedirection = Vector2.zero;
    }

    void Update()
    {
        //Turn();
    }

    private void Turn()
    {
        float angle = Mathf.Atan2(-transform.position.y + target.transform.position.y, -transform.position.x + target.transform.position.x) * Mathf.Rad2Deg;

        contoller.MoveRotation(angle*turnSpeed);
    }
    private void FixedUpdate()
    {
        Movedirection = transform.right;
        
        contoller.velocity = Movedirection*Speed;
    }
}
