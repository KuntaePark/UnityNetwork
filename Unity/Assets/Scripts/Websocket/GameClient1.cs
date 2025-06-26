using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

using DataForm;

/*
 * 게임 1 전용 웹소켓 연결
 */

public class GameClient1 : WebSocketClient
{
    private string sessionId; //서버로부터 배정받은 세션 ID
    private string playerId; //서버로부터 배정받은 플레이어 ID


    // Start is called before the first frame update
    void Start()
    {
        //서버 연결
        startConnection("ws://192.168.0.51:7778");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void handlePacket(string type, string payload)
    {
        switch (type) 
        {
            case "createSession":
                //서버로부터 세션 ID를 받았을 때
                Dictionary<string, string> sessionData = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload);
                sessionId = sessionData["sessionId"];
                playerId = sessionData["playerId"];
                Debug.Log("Session created with ID: " + sessionId + ", Player ID: " + playerId);
                break;
            case "gameState":
                //게임 상태 업데이트를 받았을 때
                Debug.Log("received game state");
                break;
            default:
                Debug.LogWarning("Unknown packet type: " + type);
                break;
        }

    }
}
