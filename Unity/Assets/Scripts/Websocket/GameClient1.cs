using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

using DataForm;

/*
 * ���� 1 ���� ������ ����
 */

public class GameClient1 : WebSocketClient
{
    private string sessionId; //�����κ��� �������� ���� ID
    private string playerId; //�����κ��� �������� �÷��̾� ID


    // Start is called before the first frame update
    void Start()
    {
        //���� ����
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
                //�����κ��� ���� ID�� �޾��� ��
                Dictionary<string, string> sessionData = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload);
                sessionId = sessionData["sessionId"];
                playerId = sessionData["playerId"];
                Debug.Log("Session created with ID: " + sessionId + ", Player ID: " + playerId);
                break;
            case "gameState":
                //���� ���� ������Ʈ�� �޾��� ��
                Debug.Log("received game state");
                break;
            default:
                Debug.LogWarning("Unknown packet type: " + type);
                break;
        }

    }
}
