﻿using System;
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
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private GameObject oldtarget;
    [SerializeField]
    private int WaypointRange = 6;
    [SerializeField]
    private GameObject Player;


    private bool inFireDistance;
    private float Timer;

    public bool IsInFireDistance { get { return inFireDistance; } set { inFireDistance = value; } }

    private void Start()
    {
        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Player = GameObject.Find("Player(Clone)");
        Movedirection = Vector2.zero;
        FindFirstTarget();
        Turn();
    }

    private void FindFirstTarget()
    {
        int RndX = UnityEngine.Random.Range((int)this.transform.position.x - WaypointRange, (int)this.transform.position.x + WaypointRange);
        int RndY = UnityEngine.Random.Range((int)this.transform.position.y - WaypointRange, (int)this.transform.position.y + WaypointRange);
        if ((RndX > 0 && RndX < gManager.levelMap.RandomMap.GetLength(0)) && (RndY > 0 && RndY < gManager.levelMap.RandomMap.GetLength(1)))
        {
            if (gManager.levelMap.RandomMap[RndX, RndY] != 1)
            {
                GameObject temp = Instantiate(targetPrefab, new Vector3(RndX, RndY), Quaternion.identity) as GameObject;

                target = temp;
                oldtarget = temp;
            }
            else
            {
                FindFirstTarget();
            }
        }
        else
        {
            FindFirstTarget();
        }
    }


    private void Update()
    {
        checkPlayerIsInRange();
        if (target != null && target != Player)
        {
            Timer += Time.deltaTime;
            Distance = Vector3.Distance(target.transform.position, transform.position);
            if (Distance < 1f || Timer > 6)
            {
                NextWaypoint();
                Turn();
                Timer = 0;
            }
        }
        else
        {
            Turn();
        }
    }

    private void checkPlayerIsInRange()
    {
        if (Player == null)
        {
            FindThePlayer();
        }
        if (Vector3.Distance(Player.transform.position, this.transform.position) < 10)
        {
            IsInFireDistance = true;
            target = Player;
        }
        else
        {
            IsInFireDistance = false;
            target = oldtarget;
            NextWaypoint();
        }
    }

    private void FindThePlayer()
    {
        Player = GameObject.Find("Player(Clone)");
    }

    private void Turn()
    {
        float angle = Mathf.Atan2(-transform.position.y + target.transform.position.y, -transform.position.x + target.transform.position.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void FixedUpdate()
    {
        contoller.velocity = Vector2.zero;
        if (!IsInFireDistance)
        {
            MoveTo();
        }
    }

    private void NextWaypoint()
    {
        int RndX = UnityEngine.Random.Range((int)this.transform.position.x - WaypointRange, (int)this.transform.position.x + WaypointRange);
        int RndY = UnityEngine.Random.Range((int)this.transform.position.y - WaypointRange, (int)this.transform.position.y + WaypointRange);
        if ((RndX > 0 && RndX < gManager.levelMap.RandomMap.GetLength(0)) && (RndY > 0 && RndY < gManager.levelMap.RandomMap.GetLength(1)))
        {
            if (gManager.levelMap.RandomMap[RndX, RndY] == 1)
            {
                target.transform.position = new Vector3(RndX, RndY);
            }
            else
            {
                NextWaypoint();
            }
        }
        else
        {
            NextWaypoint();
        }
    }

    private void MoveTo()
    {
        Movedirection = transform.right;

        contoller.velocity = Movedirection * Speed;
    }
}
