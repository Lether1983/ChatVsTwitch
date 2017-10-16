using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VoteManager : MonoBehaviour
{
    public GameManager gmanager;
    public Dictionary<int, List<VoteObject>> LevelVotePlaner;
    public List<LevelObject> LevelList;
    [SerializeField]
    List<VoteObject> ActiveLevel;
    Dictionary<int, string> deciderDict;
    bool VoteDone;
    [SerializeField]
    int changeValue;
    bool GameStart = false;
    int count;
    [SerializeField]
    float VoteActiveTime;
    private float Timer;

    public void GameBegin()
    {
        LevelVotePlaner = new Dictionary<int, List<VoteObject>>();
        deciderDict = new Dictionary<int, string>();
        for (int i = 0; i < LevelList.Count; i++)
        {
            LevelVotePlaner.Add(LevelList[i].Levelcount, LevelList[i].VoteInLevel);
        }
        LevelStart(1);
    }

    public void LevelStart(int Levelcount)
    {
        GetLevelList(Levelcount);
        StartVoteSystem();
    }

    private void StartVoteSystem()
    {
        count = 0;
        gmanager.activeVote = ActiveLevel[count];
        gmanager.setVoteUI();
        count++;
        GameStart = true;
    }

    private void GetLevelList(int Levelcount)
    {
        foreach (var item in LevelVotePlaner)
        {
            if (item.Key == Levelcount)
            {
                ActiveLevel = LevelVotePlaner[item.Key];
            }
        }
    }
    private void Update()
    {
        if (GameStart)
        {
            Timer += Time.deltaTime;
            if (Timer >= VoteActiveTime - 1)
            {
                decideTheVoteResult();
                gmanager.resetVoteUi();
                Timer = 0;
                if (count < ActiveLevel.Count)
                {
                    gmanager.activeVote = ActiveLevel[count];
                    gmanager.setVoteUI();
                    count++;
                }
            }
        }
    }

    public void decideTheVoteResult()
    {

        deciderDict.Clear();

        deciderDict.Add(gmanager.activeVote.Answercount1, gmanager.activeVote.Answer1);

        if (gmanager.activeVote.Answercount2 != gmanager.activeVote.Answercount1)
        {
            deciderDict.Add(gmanager.activeVote.Answercount2, gmanager.activeVote.Answer2);
        }

        if (gmanager.activeVote.Answercount3 != gmanager.activeVote.Answercount1 && gmanager.activeVote.Answercount3 != gmanager.activeVote.Answercount2)
        {
            deciderDict.Add(gmanager.activeVote.Answercount3, gmanager.activeVote.Answer3);
        }

        var list = deciderDict.Keys.ToList();

        list.Sort();

        var item = deciderDict[list[list.Count - 1]];

        //TODO: Add the Logic for the Standard VoteObjects
        if (gmanager.activeVote is CharakterVoteObject)
        {
            if (item == "heavy")
            {
                if (gmanager.activeVote.TypeofEntity == "Enemy")
                {
                    for (int i = 0; i < gmanager.EnemyList.Count; i++)
                    {
                        //TODO: Test it
                        gmanager.EnemyList[i].ModifyDecorator(gmanager.activeVote.Classname, "heavy");
                    }
                }
                else
                {
                    //TODO: Test it
                    gmanager.player.ModifyDecorater(gmanager.activeVote.Classname, "heavy");
                }
            }
            else if (item == "normal") {/*Nothing happend*/}
            else if (item == "light")
            {
                if (gmanager.activeVote.TypeofEntity == "Enemy")
                {
                    for (int i = 0; i < gmanager.EnemyList.Count; i++)
                    {
                    //TODO: Test it
                        gmanager.EnemyList[i].ModifyDecorator(gmanager.activeVote.Classname, "light");
                    }
                }
                else
                {
                //TODO: Test it
                    gmanager.player.ModifyDecorater(gmanager.activeVote.Classname, "light");
                }
            }
            else if (item == "yes")
            {
                if (gmanager.activeVote.TypeofEntity == "Enemy")
                {
                    for (int i = 0; i < gmanager.EnemyList.Count; i++)
                    {
                        //TODO: Test it
                        var type = Type.GetType(gmanager.activeVote.Classname);
                        gmanager.EnemyList[i].AddDecorator((IEntityGenerator)Activator.CreateInstance(type));
                    }
                }
                else
                {
                    //TODO: Test it
                    var type = Type.GetType(gmanager.activeVote.Classname);
                    gmanager.player.AddDecorator((IEntityGenerator)Activator.CreateInstance(type));
                }
            }
            else if (item == "no") {/*nothing happend*/}
            else if (item == "change") {/*TODO: Modfiy the Uniform*/ }

        }
        else if (gmanager.activeVote is EnviromentVoteObject)
        {
            if (item == "more")
            {
                gmanager.levelMap.ModifyDecorater(gmanager.activeVote.Classname, changeValue);
                if (gmanager.activeVote.name == "AirStrikeVote")
                {
                    //TODO: send more as one Airstrike
                }
            }
            else if (item == "equal") {/*Nothing happend*/}
            else if (item == "less")
            {
                gmanager.levelMap.ModifyDecorater(gmanager.activeVote.Classname, -changeValue);
            }
            else if (item == "yes")
            {
                if (gmanager.activeVote.name == "RebelVote")
                {

                    
                    gmanager.SManager.SpawnRebelsNearPlayer();
                }
                else
                {
                    var type = Type.GetType(gmanager.activeVote.Classname);
                    gmanager.levelMap.AddDecorater((IMapGenerator)Activator.CreateInstance(type));
                }
            }
            else if (item == "no") {/*nothing happend*/}
            else if (item == "fake")
            {
                //TODO: Send fake Packet
            }
            else if (item == "enemy")
            {
                //TODO: Test it
                gmanager.SManager.SpawnEnemyNearPlayer();
            }
        }
        else if (gmanager.activeVote is BiomVoteObject)
        {
            if (item == "forest" || item == "jungle") { /*Change Nothing*/ }
            else if (item == "snow" || item == "swamp")
            {
                foreach (var Generator in gmanager.levelMap.ActiveMap)
                {
                    if (Generator == typeof(StoneGenerator))
                    {
                        gmanager.levelMap.RemoveDecorater(Generator);
                    }
                }
            }
            else if (item == "sand" || item == "ruins")
            {
                foreach (var Generator in gmanager.levelMap.ActiveMap)
                {
                    if (Generator == typeof(TreeGenerator))
                    {
                        gmanager.levelMap.RemoveDecorater(Generator);
                    }
                }
            }
        }
        gmanager.activeVote.Answercount1 = 0;
        gmanager.activeVote.Answercount2 = 0;
        gmanager.activeVote.Answercount3 = 0;
        gmanager.activeVote.Totalcount = 0;
    }
}
