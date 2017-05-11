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
        if (gmanager.activeVote.GetType() == typeof(CharakterVoteObject))
        {
            if (item == "heavy")
            {

            }
            else if (item == "normal")
            {

            }
            else if (item == "light")
            {

            }
        }
        else if (gmanager.activeVote.GetType() == typeof(EnviromentVoteObject))
        {
            if (item == "more")
            {

            }
            else if (item == "equal")
            {

            }
            else if (item == "less")
            {

            }
        }
        else if (gmanager.activeVote.GetType() == typeof(BiomVoteObject))
        {
            if (item == "forest" || item == "jungle")
            {

            }
            else if (item == "snow" || item == "swamp")
            {

            }
            else if (item == "sand" || item == "ruins")
            {

            }
        }

        gmanager.activeVote.Answercount1 = 0;
        gmanager.activeVote.Answercount2 = 0;
        gmanager.activeVote.Answercount3 = 0;
    }
}
