using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class GameManager : MonoBehaviour, IGameManager
{
    public bool joinedTheChat;
    public string message;

    
    
    public int Try = 0;
    public IrcClient irc;
    public VoteObject activeVote;
    public List<Enemy> EnemyList;
    public Player player;
    public Map levelMap;
    public TextAsset tileSheet;
    public Container container;
    public bool CompletTarget;

    [SerializeField]
    private CaveTesselator tesselator = null;
    [SerializeField]
    private SpawnManager sManager;
    private Map currentMap;

    //TODO: Variable Dynamic for the Channels
    private string ChannelName = "lether";

    public SpawnManager SManager { get; set; }

    private void Awake()
    {
        container = JsonUtility.FromJson<Container>(tileSheet.text);
    }

    void Start()
    {
        irc = new IrcClient("irc.twitch.tv", 6667, "missionagainstchatbot", "oauth:f4rfrtulipsiiqbdoaqdb2s7nhr2ou");
        levelMap = new Map();
        FillTheFirstMap();

        //TODO: SUCCESSFULL TRY NEED A SECOND OPINION
        /*
        var type = Type.GetType(activeVote.Classname);
        levelMap.AddDecorater((IMapGenerator)Activator.CreateInstance(type));
        */
        currentMap = levelMap;
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        currentMap.CreateNewMap();
        tesselator.Tesselate();
        sManager.SpawnObjects(currentMap);
        sManager.SpawnPlayer(currentMap);
    }

    private void FillTheFirstMap()
    {
        levelMap.AddDecorater(new CaveGenerator());
        levelMap.AddDecorater(new DungeonGenerator());
        levelMap.AddDecorater(new SpawnpointGenerator());
        levelMap.AddDecorater(new ExitpointGenerator());
        levelMap.AddDecorater(new BushGenerator());
        levelMap.AddDecorater(new TreeGenerator());
        levelMap.AddDecorater(new StoneGenerator());
        levelMap.AddDecorater(new MineGenerator());
        levelMap.AddDecorater(new TrapGenerator());
    }

    public void DeleteTheCurrentMap()
    {
        GameObject[] Removal = GameObject.FindGameObjectsWithTag("Enviroment");
        for (int i = 0; i < Removal.Length; i++)
        {
            Destroy(Removal[i]);
        }
        player.SavePlayerValueAndDestroy();
    }

    public void ModifyCurrentLevel()
    {

    }

    public void ChangeToNextLevel()
    {
        GenerateLevel();
    }

    public void SendMessages()
    {
        irc.sendPublicChatMessage("Kevin ist ein Penis");
    }

    public void SendWhisper()
    {
        irc.sendPrivateWhisper("les-lee is a dick");
    }
    
    public void JoinChat()
    {
        irc.joinRoom(ChannelName);
        joinedTheChat = true;
    }
   
    void Update()
    {
        if (joinedTheChat)
        {
            message = irc.readMessages();
        }
    }
}
