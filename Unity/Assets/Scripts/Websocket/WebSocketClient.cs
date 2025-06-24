using DataForm;
using HybridWebSocket;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�� ���� Ŭ���̾�Ʈ Ŭ����.
public abstract class WebSocketClient : MonoBehaviour
{
    private WebSocket ws;

    //���� ���� �� ȣ���ϴ� �޼ҵ�. ���� URL�� ���ڷ� �޾� ������ ������ ����
    public void startConnection(string serverUrl)
    {
        ws = WebSocketFactory.CreateInstance(serverUrl);
        ws.Connect();
        ws.OnOpen += () => Debug.Log("Connected to WebSocket server at " + serverUrl);
        ws.OnMessage += Call;
    }

    private void OnDestroy()
    {
        if (ws != null)
        {
            handleClose();
            ws.Close();
            Debug.Log("WebSocket connection closed.");
        }
    }

    //�޽��� ���� ó�� �޼ҵ�. �������̵��Ͽ� ���
    public void Call(byte[] message)
    {
        string JsonData = System.Text.Encoding.UTF8.GetString(message);
        Packet packet = JsonConvert.DeserializeObject<Packet>(JsonData);
        handlePacket(packet.type, packet.payload);
    }

    //�޽��� ���� �޼ҵ�.
    public void Send(string type, string JSONMessage)
    {
        if (ws != null && ws.GetState() == WebSocketState.Open)
        {
            Packet sendPacket = new Packet
            {
                type = type,
                payload = JSONMessage
            };
            byte[] data = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sendPacket));
            ws.Send(data);
            //Debug.Log("Sent packet type: " + type + ", payload: " + JSONMessage);
        }
        else
        {
            Debug.LogWarning("WebSocket is not connected.");
        }
    }

    //���� ��Ŷ ó�� �޼ҵ�. �������̵��Ͽ� ���
    public abstract void handlePacket(string type, string payload);

    //�� ���� ���� ���� �� ȣ��Ǵ� �޼ҵ�. �⺻ ������ �ƹ� �۾��� ���� ����.
    public virtual void handleClose()
    {
        return;
    }

}
