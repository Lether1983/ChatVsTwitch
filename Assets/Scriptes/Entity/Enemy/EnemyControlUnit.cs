using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlUnit : MonoBehaviour
{
    [SerializeField]
    private GameManager gManager;
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
    [SerializeField]
    private float Distance;
    private bool inFireDistance;
    [SerializeField]
    private GameObject target;

    public bool IsInFireDistance { get { return inFireDistance; } set { inFireDistance = value; } }

    private void Start()
    {
        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Movedirection = Vector2.zero;
        FindFirstTarget();
        Turn();
    }

    private void FindFirstTarget()
    {
        int RndX = UnityEngine.Random.Range((int)this.transform.position.x - 3, (int)this.transform.position.x + 3);
        int RndY = UnityEngine.Random.Range((int)this.transform.position.y - 3, (int)this.transform.position.y + 3);
        if (gManager.levelMap.RandomMap[RndX, RndY] != 1)
        {
            GameObject temp = Instantiate(targetPrefab, new Vector3(RndX, RndY), Quaternion.identity) as GameObject;

            target = temp;
        }
        else
        {
            FindFirstTarget();
        }
    }
    private void Update()
    {
        Distance = Vector3.Distance(target.transform.position, transform.position);
        if (Distance < 1f)
        {
            NextWaypoint();
            Turn();
        }
    }
    private void Turn()
    {
        float angle = Mathf.Atan2(-transform.position.y + target.transform.position.y, -transform.position.x + target.transform.position.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void FixedUpdate()
    {
        MoveTo();
    }

    private void NextWaypoint()
    {
        int RndX = UnityEngine.Random.Range((int)this.transform.position.x - 3, (int)this.transform.position.x + 3);
        int RndY = UnityEngine.Random.Range((int)this.transform.position.y - 3, (int)this.transform.position.y + 3);
        //if (gManager.levelMap.RandomMap[RndX, RndY] != 1)
        //{
            target.transform.position = new Vector3(RndX, RndY);
        //}
        //else
        //{
        //    NextWaypoint();
        //}
    }

    private void MoveTo()
    {
        Movedirection = transform.right;

        contoller.velocity = Movedirection * Speed;
    }
}
