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
        startConnection("ws://localhost:7778");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void handleOpen()
    {
        //세션이 이미 생성되어 있는 상태에서 해당 세션에 입장 요청
        string message = JsonConvert.SerializeObject(new { sessionId, playerId });
        Debug.Log(message);
        Send("enterSession", message);
    }

    public override void handlePacket(string type, string payload)
    {
        switch (type) 
        {
            case "sessionId":
                //서버로부터 세션 ID를 받았을 때
                sessionId = payload;
                Debug.Log("Received session ID: " + sessionId);
                break;
            default:
                Debug.LogWarning("Unknown packet type: " + type);
                break;
        }

    }
}
