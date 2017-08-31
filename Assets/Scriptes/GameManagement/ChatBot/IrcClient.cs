using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.IO;

public class IrcClient
{
    public string UserName;
    private const string botName = "MissionAgainstChatBot";
    private string channel;
    private string targetUser = "lether";

    private TcpClient tcpClient;
    private StreamReader inputStream;
    private StreamWriter outputStream;


    //Password oauth:z60l0jp1fltf57q55bar0c50xs7f9n
    public IrcClient(string ip, int port, string userName, string password)
    {
        this.UserName = userName;

        tcpClient = new TcpClient(ip, port);
        inputStream = new StreamReader(tcpClient.GetStream());
        outputStream = new StreamWriter(tcpClient.GetStream());

        outputStream.WriteLine("PASS " + password);
        outputStream.WriteLine("NICK " + userName);
        outputStream.WriteLine("USER " + userName + " 8 * :" + userName);
        outputStream.WriteLine("CAP REQ :twitch.tv/commands");
        outputStream.Flush();
    }

    public void joinRoom(string channel)
    {
        this.channel = channel;
        outputStream.WriteLine("JOIN #" + channel);
        outputStream.WriteLine("JOIN #jtv");
        outputStream.Flush();
    }


    internal string readMessages()
    {
        if (tcpClient.Available == 0) return string.Empty;
        string read = inputStream.ReadLine();
        if (read.StartsWith("PING"))
        {
            sendIrcMessage("PONG" + read.Substring(4));
            return string.Empty;
        }
        Debug.Log(read);
        var filteredMessage = read.Substring(read.IndexOf(' ') + 1);
        var messageType = filteredMessage.Substring(0, filteredMessage.IndexOf(' '));
        switch (messageType)
        {
            case "JOIN":
                return string.Empty;
            case "PRIVMSG":
                return filteredMessage.Substring(filteredMessage.IndexOf(':') + 1);
            case "WHISPER":
                Debug.Log("shht here");
                return string.Empty;
            default:
                Debug.LogWarningFormat("Unhandled Message Type {0}: {1}", messageType, filteredMessage);
                return string.Empty;
        }
    }

    public void sendIrcMessage(string message)
    {
        outputStream.WriteLine(message);
        outputStream.Flush();
    }

    public void sendPublicChatMessage(string message)
    {
        sendIrcMessage(":" + UserName + "!" + UserName + "@" + UserName +
                ".tmi.twitch.tv PRIVMSG #" + channel + " :" + message);
    }

    public void sendPrivateWhisper(string message)
    {
        sendIrcMessage(":" +UserName+"~"+UserName+"@"+UserName+
                ".tmi.twitch.tv PRIVMSG #jtv : /w "+targetUser+" "+ message);
    }
}
