using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DataForm;

/*
 * 게임 1 전용 웹소켓 연결
 */

public class GameClient1 : WebSocketClient
{
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

    public override void handlePacket(string type, string payload)
    {
        throw new System.NotImplementedException();
    }
}
