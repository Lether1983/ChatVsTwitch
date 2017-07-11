using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject VoteShowObject;
    [SerializeField]
    private SpawnManager Smanager;

    private Player activePlayer;


    [SerializeField]
    private Text CurrenAmmoCount;
    [SerializeField]
    private Text MaxAmmoCount;
    [SerializeField]
    private Text CurrentHealth;
    [SerializeField]
    private Text MissionCount;
    [SerializeField]
    private Text LifeCount;
    [SerializeField]
    private GameObject VotePanel;
    private VoteObject ActiveVoteObject;

    public VoteObject activeVoteObject
    {
        get { return ActiveVoteObject; }
        set { ActiveVoteObject = value; }
    }


    private GameObject ActiveVote;

    private void Update()
    {
        if(activePlayer == null && Smanager.GetPlayer != null)
        {
            activePlayer = Smanager.GetPlayer.GetComponent<Player>();     
        }
        if(activePlayer != null)
        {
            CurrentHealth.text = activePlayer.Health.ToString();
            LifeCount.text = activePlayer.Lifes.ToString();
        }
    }

    public void ActivateVote()
    {
        ActiveVote = Instantiate(VoteShowObject, new Vector3(0, 0), Quaternion.identity) as GameObject;
        ActiveVote.transform.SetParent(VotePanel.transform);
    }

    internal void resetVoteUi()
    {
        Destroy(ActiveVote);
             
    }
}
