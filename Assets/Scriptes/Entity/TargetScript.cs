using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetScript : MonoBehaviour
{
    [SerializeField]
    public GameManager gManager;

    private void Start()
    {
        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}
