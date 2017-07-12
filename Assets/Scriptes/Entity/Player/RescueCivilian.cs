using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueCivilian : MonoBehaviour
{
    [SerializeField]
    private bool active;
    [SerializeField]
    private List<GameObject> Civilian;
    private GameManager gManager;

    private GameObject civilianToADD;

    public GameObject CivilianToAdd
    {
        get { return civilianToADD; }
        set { civilianToADD = value; }
    }

    public bool Active { get { return active; } set { active = value; } }

    private void Start()
    {
        gManager = GetComponent<Player>().gmanager;
    }
    private void Update()
    {
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Civilian.Add(CivilianToAdd);
                gManager.CompletTarget = true;

            }
        }
    }
}
