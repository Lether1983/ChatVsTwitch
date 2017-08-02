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
    //TODO: Instansiation and Logic for LifeObjects
    [SerializeField]
    private GameObject LifePanel;
    [SerializeField]
    private GameObject VotePanel;

    private VoteObject ActiveVoteObject;

    private GameObject ActiveVote;

    public VoteObject activeVoteObject
    {
        get { return ActiveVoteObject; }
        set { ActiveVoteObject = value; }
    }

    private void Update()
    {
        if(activePlayer == null && Smanager.GetPlayer != null)
        {
            activePlayer = Smanager.GetPlayer.GetComponent<Player>();     
        }
        if(activePlayer != null)
        {
            CurrentHealth.text = activePlayer.Health.ToString();
            CurrenAmmoCount.text = activePlayer.GetCurrenAmmo().ToString();
            MaxAmmoCount.text = activePlayer.GetMaxAmmo().ToString();
            //LifeCount.text = activePlayer.Lifes.ToString();
        }
        if(ActiveVote != null)
        {
            ActiveVote.GetComponent<VoteValueHolder>().AnswerCount.text = ActiveVoteObject.Totalcount.ToString();
        }

    }

    public void ActivateVote()
    {
        ActiveVote = Instantiate(VoteShowObject, new Vector3(0, 0), Quaternion.identity) as GameObject;
        ActiveVote.transform.SetParent(VotePanel.transform);
        ActiveVote.GetComponent<VoteValueHolder>().Question.text = ActiveVoteObject.Question;
        ActiveVote.GetComponent<VoteValueHolder>().Answer1.text = ActiveVoteObject.Answer1;
        ActiveVote.GetComponent<VoteValueHolder>().Answer2.text = ActiveVoteObject.Answer2;
        ActiveVote.GetComponent<VoteValueHolder>().Answer3.text = ActiveVoteObject.Answer3;
    }

    internal void resetVoteUi()
    {
        Destroy(ActiveVote);
    }
}
