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

    }
}
