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

    public string PlayerId { get; private set; } //�÷��̾� ID (�����κ��� ��������)
    public Game1Manager game1Manager; //���� �Ŵ��� ��ũ��Ʈ ����


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
                PlayerId = sessionData["playerId"];
                Debug.Log("Session created with ID: " + sessionId + ", Player ID: " + PlayerId);
                break;
            case "gameState":
                //���� ���� ������Ʈ�� �޾��� ��
                Debug.Log("received: " + payload);
                game1Manager.UpdateGameState(payload);
                break;
            default:
                Debug.LogWarning("Unknown packet type: " + type);
                break;
        }

    }
}
