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
    public Map levelMap;

    public TextAsset tileSheet;
    public Container container;
    public MeshGenerator meshGenerator;

    //TODO: Variable Dynamic for the Channels
    private string ChannelName = "lether";


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
}
