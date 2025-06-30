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

    public string PlayerId { get; private set; } //플레이어 ID (서버로부터 배정받음)
    public Game1Manager game1Manager; //게임 매니저 스크립트 참조


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
                PlayerId = sessionData["playerId"];
                Debug.Log("Session created with ID: " + sessionId + ", Player ID: " + PlayerId);
                break;
            case "gameState":
                //게임 상태 업데이트를 받았을 때
                Debug.Log("received: " + payload);
                game1Manager.UpdateGameState(payload);
                break;
            default:
                Debug.LogWarning("Unknown packet type: " + type);
                break;
        }

    }
}
