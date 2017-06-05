using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VoteManager : MonoBehaviour
{
    public GameManager gmanager;
    public Dictionary<int, List<VoteObject>> LevelVotePlaner;

    Dictionary<int, string> deciderDict;
    
    
    //TODO: ADD some Functions to the VoteManager
    public void decideTheVoteResult()
    {
        deciderDict = new Dictionary<int, string>();

        deciderDict.Add(gmanager.activeVote.Answercount1, gmanager.activeVote.Answer1);
        deciderDict.Add(gmanager.activeVote.Answercount2, gmanager.activeVote.Answer2);
        deciderDict.Add(gmanager.activeVote.Answercount3, gmanager.activeVote.Answer3);

        var list = deciderDict.Keys.ToList();
        list.Sort();
        Debug.Log(list[2]);
        var item = deciderDict[list[2]];
        Debug.Log(item);
        //TODO: Add the Logic for the Standard VoteObjects
        if (gmanager.activeVote is CharakterVoteObject)
        {
            if (item == "heavy")
            {
                //Change the Standard Value + The Change Value
            }
            else if (item == "normal")
            {
                //Nothing happend
            }
            else if (item == "light")
            {
                //Change the Standard Value - The Change Value
            }
            else if(item == "yes")
            {
                
            }
            else if(item == "no")
            {
                //nothing happend
            }
        }
        else if (gmanager.activeVote is EnviromentVoteObject)
        {
            if (item == "more")
            {
                //Change the Standard Value + The Change Value
            }
            else if (item == "equal")
            {
                //Nothing happend
            }
            else if (item == "less")
            {
                //Change the Standard Value - The Change Value
            }
            else if (item == "yes")
            {
                //gmanager.levelMap.AddDecorater()
            }
            else if (item == "no")
            {
                //nothing happend
            }
        }
        else if (gmanager.activeVote is BiomVoteObject)
        {
            if (item == "forest" || item == "jungle")
            {
                // Standard Generation with all Generators
            }
            else if (item == "snow" || item == "swamp")
            {
                // Without Stones
            }
            else if (item == "sand" || item == "ruins")
            {
                //Without Trees
            }
            
        }

        gmanager.activeVote.Answercount1 = 0;
        gmanager.activeVote.Answercount2 = 0;
        gmanager.activeVote.Answercount3 = 0;
    }
}
