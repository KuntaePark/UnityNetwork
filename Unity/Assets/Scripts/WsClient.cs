using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using DataForm;

//plugin for websocket support in webgl builds
using HybridWebSocket;

public class WsClient : MonoBehaviour
{
    // Start is called before the first frame update
    private WebSocket ws;
    private string playerId;
    public string PlayerId{ get; set; }

    private PlayerManager playerManager;

    void Start()
    {
        ws = WebSocketFactory.CreateInstance("ws://192.168.0.51:7777");
        ws.Connect();

        ws.OnOpen += () => Debug.Log("Connected");
        ws.OnMessage += Call;

        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
    }

    private void OnDestroy()
    {
        if(ws != null)
        {
            ws.Close();
            Debug.Log("WebSocket connection closed.");
        }
    }
    void Call(byte[] message)
    {
        string JsonData = System.Text.Encoding.UTF8.GetString(message);
        Packet packet = JsonConvert.DeserializeObject<Packet>(JsonData);
        switch(packet.type)
        {
            case "playerId":
                //receive assigned player ID
                PlayerId = packet.payload;
                Debug.Log("Assigned Player ID: " + PlayerId);
                break;
            case "playerUpdate":
                //handle player update
                playerManager.updatePlayers(packet.payload);
                break;
            case "playerExit":
                playerManager.exitPlayer(packet.payload);
                break;
            default:
                Debug.LogWarning("Unknown packet type: " + packet.type);
                break;
        }
    }

    public void Send(string type, string JSONMessage)
    {
        if(ws != null && ws.GetState() == WebSocketState.Open)
        {
            Packet sendPacket = new Packet
            {
                type = type,
                payload = JSONMessage
            };
            byte[] data = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sendPacket));
            ws.Send(data);
            Debug.Log("Sent packet type: " + type + ", payload: " + JSONMessage);
        }
        else
        {
            Debug.LogWarning("WebSocket is not connected.");
        }
    }
}
