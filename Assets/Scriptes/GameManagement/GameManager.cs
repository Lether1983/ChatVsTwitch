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
    public bool VoteDone;
    public List<GameObject> BulletPool;
    public List<GameObject> EnemyBulletPool;

    [SerializeField]
    private GameObject SpritePrefab;
    [SerializeField]
    private CaveTesselator tesselator = null;
    [SerializeField]
    private SpawnManager sManager;
    [SerializeField]
    private VoteManager vManager;
    [SerializeField]
    private UIController UIController;
    [SerializeField]
    private Map currentMap;
    private int activeVoteCount;

    private string ChannelName = "";

    public SpawnManager SManager { get { return sManager; } set { sManager = value; } }
    public Map CurrentMap { get { return currentMap; } set { currentMap = value; } }

    private void Awake()
    {
        container = JsonUtility.FromJson<Container>(tileSheet.text);
    }

    internal void setVoteUI()
    {
        UIController.activeVoteObject = activeVote;
        UIController.ActivateVote();
    }

    internal void resetVoteUi()
    {
        UIController.activeVoteObject = activeVote;
        UIController.resetVoteUi();
    }

    void Start()
    {
        for (int i = 0; i < 50; i++)
        {
            GameObject Temp = Instantiate(SpritePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            Temp.SetActive(false);
            EnemyBulletPool.Add(Temp);
        }
        ChannelName = GameObject.Find("ValueHolder").GetComponent<ValueHolderScript>().TwitchName;
        irc = new IrcClient("irc.twitch.tv", 6667, "missionagainstchatbot", "oauth:f4rfrtulipsiiqbdoaqdb2s7nhr2ou");
        JoinChat();
        levelMap = new Map();
        FillTheFirstMap();

        currentMap = levelMap;
        GenerateLevel();
        vManager.GameBegin();
    }

    private void GenerateLevel()
    {
        currentMap.CreateNewMap();
        tesselator.Tesselate();
        sManager.SpawnObjects(currentMap);
        sManager.SpawnPlayer(currentMap);
        sManager.SpawnEnemys(currentMap);
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
        levelMap.AddDecorater(new EnemyGenerator());
    }


    public void DeleteTheCurrentMap()
    {
        GameObject[] Removal = GameObject.FindGameObjectsWithTag("Enviroment");
        for (int i = 0; i < Removal.Length; i++)
        {
            Destroy(Removal[i]);
        }
        player.SavePlayerValueAndDestroy();
        player.DestroyObject();
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
