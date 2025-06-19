using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class WsClient : MonoBehaviour
{
    // Start is called before the first frame update
    private WebSocket ws;
    private string playerId;
    public string PlayerId{ get; set; }

    [System.Serializable]
    public class Packet
    {
        public string type;
        public string payload;
    }

    private PlayerManager playerManager;

    void Start()
    {
        ws = new WebSocket("ws://localhost:7777");
        ws.Connect();

        ws.OnOpen += (sender, e) => Debug.Log("Connected to " + ((WebSocket)sender).Url);
        ws.OnMessage += Call;

        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
    }

    void Call(object sender, MessageEventArgs e)
    {
        //Debug.Log("주소 :  " + ((WebSocket)sender).Url + ", 데이터 : " + e.Data);
        Packet packet = JsonUtility.FromJson<Packet>(e.Data);
        switch(packet.type)
        {
            case "playerId":
                //receive assigned player ID
                PlayerId = packet.payload;
                Debug.Log("Assigned Player ID: " + PlayerId);
                //if assigned, create a player object
                //playerManager.createSelf(PlayerId);
                break;
            case "playerUpdate":
                //handle player update
                playerManager.updatePlayers(packet.payload);
                break;
            default:
                Debug.LogWarning("Unknown packet type: " + packet.type);
                break;
        }
    }

    public void Send(string type, string JSONMessage)
    {
        if(ws != null && ws.IsAlive)
        {
            Packet sendPacket = new Packet
            {
                type = type,
                payload = JSONMessage
            };
            ws.Send(JsonUtility.ToJson(sendPacket));
            Debug.Log("Sent packet type: " + type + ", payload: " + JSONMessage);
        }
        else
        {
            Debug.LogWarning("WebSocket is not connected.");
        }
    }
}
