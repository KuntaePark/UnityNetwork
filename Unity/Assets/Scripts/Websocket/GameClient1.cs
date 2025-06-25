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
        startConnection("ws://localhost:7778");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void handleOpen()
    {
        //������ �̹� �����Ǿ� �ִ� ���¿��� �ش� ���ǿ� ���� ��û
        string message = JsonConvert.SerializeObject(new { sessionId, playerId });
        Debug.Log(message);
        Send("enterSession", message);
    }

    public override void handlePacket(string type, string payload)
    {
        switch (type) 
        {
            case "sessionId":
                //�����κ��� ���� ID�� �޾��� ��
                sessionId = payload;
                Debug.Log("Received session ID: " + sessionId);
                break;
            default:
                Debug.LogWarning("Unknown packet type: " + type);
                break;
        }

    }
}
