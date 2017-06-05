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

        //TODO: SUCCESSFULL TRY NEED A SECOND OPINION
        /*
        var type = Type.GetType(activeVote.Classname);
        levelMap.AddDecorater((IMapGenerator)Activator.CreateInstance(type));
        */

        levelMap.CreateNewMap();
        tesselator.Tesselate();
        for (int x = 0; x < levelMap.MapWidth; x++)
        {
            for (int y = 0; y < levelMap.MapHeight; y++)
            {
                if ((levelMap.Get(x, y) & 4) > 0)
                {
                    var entity = levelMap.Get(x, y) >> 10;
                   
                    switch (entity)
                    {
                        case 1: // door
                            break;
                        case 2: // dpol
                            break;
                        case 4: // Mine
                            break;
                        case 8: // Target
                            break;
                        case 16: // Bush
                            break;
                        case 32: // Tree
                            break;
                        case 64: // Stone
                            break;
                        case 128: // Trap
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        //meshGenerator.CreateMesh(levelMap);
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
