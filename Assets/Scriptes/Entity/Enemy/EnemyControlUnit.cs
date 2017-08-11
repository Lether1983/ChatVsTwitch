using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlUnit : MonoBehaviour
{
    [SerializeField]
    private GameManager gmanager;
    [SerializeField]
    private GameObject targetPrefab;
    [SerializeField]
    private Rigidbody2D contoller;
    [SerializeField]
    private Vector2 Movedirection;
    [SerializeField]
    private float Speed;
    [SerializeField]
    private float turnSpeed;
    private bool inFireDistance;
    private GameObject target;

    public bool IsInFireDistance { get { return inFireDistance; } set { inFireDistance = value; } }

    private void Start()
    {
        Movedirection = Vector2.zero;
        FindFirstTarget();
    }

    private void FindFirstTarget()
    {
        int RndX = UnityEngine.Random.Range((int)this.transform.position.x - 3,(int) this.transform.position.x + 3);
        int RndY = UnityEngine.Random.Range((int)this.transform.position.y - 3, (int)this.transform.position.y + 3);
        GameObject temp = Instantiate(targetPrefab, new Vector3(RndX, RndY), Quaternion.identity)as GameObject;
        target = temp;
    }

    void Update()
    {
        Turn();
    }

    private void Turn()
    {
        float angle = Mathf.Atan2(-transform.position.y + target.transform.position.y, -transform.position.x + target.transform.position.x) * Mathf.Rad2Deg;

        contoller.MoveRotation(angle * turnSpeed);
    }
    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, target.transform.position) < 0.5f)
        {
            NextWaypoint();
        }
        else
        {
            MoveTo();
        }
    }

    private void NextWaypoint()
    {
        int RndX = UnityEngine.Random.Range((int)this.transform.position.x - 3, (int)this.transform.position.x + 3);
        int RndY = UnityEngine.Random.Range((int)this.transform.position.y - 3, (int)this.transform.position.y + 3);
        target.transform.position = new Vector2(RndX, RndY);
    }

    private void MoveTo()
    {
        Movedirection = transform.right;

        contoller.velocity = Movedirection * Speed;
    }
}
