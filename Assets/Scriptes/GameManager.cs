using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class GameManager : MonoBehaviour , IGameManager
{

    public bool joinedTheChat;
    public string message;
    public IrcClient irc;
    public VoteObject activeVote;
    public List<Enemy> EnemyList;
    public Player player;
    public GameObject[,] map;

    public TextAsset tileSheet;
    public Container container;
    public MeshGenerator meshGenerator;

    //TODO: Variable Dynamic for the Channels
    private string ChannelName = "lether";
    Dictionary<int, string> deciderDict;
    private Map levelMap;

    private void Awake()
    {
        container = JsonUtility.FromJson<Container>(tileSheet.text);
    }

    void Start()
    {
        irc = new IrcClient("irc.twitch.tv", 6667, "missionagainstchatbot", "oauth:f4rfrtulipsiiqbdoaqdb2s7nhr2ou");
        levelMap = new Map();
        levelMap.AddDecorater(new CaveGenerator());
        levelMap.AddDecorater(new DungeonGenerator());
        levelMap.AddDecorater(new BushGenerator());
        levelMap.AddDecorater(new TreeGenerator());
        levelMap.AddDecorater(new StoneGenerator());
        levelMap.CreateNewMap();
        meshGenerator.CreateMesh(levelMap);
    }

    public void SendMessages()
    {
        irc.sendPublicChatMessage("Kevin ist ein Penis");
    }

    public void JoinChat()
    {
        irc.joinRoom(ChannelName);
        joinedTheChat = true;
    }

    void Update()
    {
        if(joinedTheChat)
        {
            message = irc.readMessages();
        }
    }

    public void decideTheVoteResult()
    {
        deciderDict = new Dictionary<int, string>();

        deciderDict.Add(activeVote.Answercount1, activeVote.Answer1 );
        deciderDict.Add(activeVote.Answercount2,activeVote.Answer2 );
        deciderDict.Add(activeVote.Answercount3,activeVote.Answer3);

        var list = deciderDict.Keys.ToList();
        list.Sort();
        Debug.Log(list[2]);
        var item = deciderDict[list[2]];
                Debug.Log(item);
        //TODO: Add the Logic for the Standard VoteObjects
        if(activeVote.GetType() == typeof(CharakterVoteObject))
        {
            if(activeVote.Answer1 == "heavy")
            {

            }
            else if(activeVote.Answer2 == "normal")
            {

            }
            else if(activeVote.Answer3 == "light")
            {

            }
        }
        else if (activeVote.GetType() == typeof(EnviromentVoteObject))
        {
            if (activeVote.Answer1 == "more")
            {

            }
            else if (activeVote.Answer2 == "equal")
            {

            }
            else if (activeVote.Answer3 == "less")
            {

            }
        }
        else if (activeVote.GetType() == typeof(BiomVoteObject))
        {
            if (activeVote.Answer1 == "forest"|| activeVote.Answer1 == "jungle")
            {

            }
            else if (activeVote.Answer2 == "snow"|| activeVote.Answer2 == "swamp")
            {

            }
            else if (activeVote.Answer3 == "sand"|| activeVote.Answer3 == "ruins")
            {

            }
        }

        activeVote.Answercount1 = 0;
        activeVote.Answercount2 = 0;
        activeVote.Answercount3 = 0;
    }
}
