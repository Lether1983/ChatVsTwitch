using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
   
    public GameObject Player;
    private Vector3 Offset;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Player(Clone)");
        Offset.x = transform.position.x - Player.transform.position.x+1.5f;
        Offset.y = transform.position.y - Player.transform.position.y;
        Offset.z = transform.position.z - Player.transform.position.z;
    }

    private void LateUpdate()
    {
        if (Player == null)
        {
            Player = GameObject.Find("Player(Clone)");
        }
        else
        {
            transform.position = Player.transform.position + Offset;
        }
    }
}
