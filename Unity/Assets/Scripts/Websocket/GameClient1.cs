using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DataForm;

/*
 * ���� 1 ���� ������ ����
 */

public class GameClient1 : WebSocketClient
{
    // Start is called before the first frame update
    void Start()
    {
        //���� ����
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
