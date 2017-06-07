using UnityEngine;
using System;
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
    [SerializeField]
    private CaveTesselator tesselator = null;
    [SerializeField]
    private SpawnManager sManager;

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
        levelMap.AddDecorater(new SpawnpointGenerator());
        levelMap.AddDecorater(new ExitpointGenerator());
        levelMap.AddDecorater(new BushGenerator());
        levelMap.AddDecorater(new TreeGenerator());
        levelMap.AddDecorater(new StoneGenerator());

        //TODO: SUCCESSFULL TRY NEED A SECOND OPINION
        /*
        var type = Type.GetType(activeVote.Classname);
        levelMap.AddDecorater((IMapGenerator)Activator.CreateInstance(type));
        */

        levelMap.CreateNewMap();
        tesselator.Tesselate();
        sManager.SpawnObjects(levelMap);
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
