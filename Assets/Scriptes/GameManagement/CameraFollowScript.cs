using System.Collections;
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
        Offset = transform.position - Player.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = Player.transform.position + Offset;
    }
}
