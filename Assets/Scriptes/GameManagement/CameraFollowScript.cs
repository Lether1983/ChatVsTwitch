﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    private Vector3 Offset;

    // Use this for initialization
    void Start()
    {
        Offset.x = transform.position.x - Player.transform.position.x+1.5f;
        Offset.y = transform.position.y - Player.transform.position.y;
        Offset.z = transform.position.z - Player.transform.position.z;
    }

    private void LateUpdate()
    {
        transform.position = Player.transform.position + Offset;
    }
}
