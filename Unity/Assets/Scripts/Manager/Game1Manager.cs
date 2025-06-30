using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataForm;
using Newtonsoft.Json;

public class Game1Manager : MonoBehaviour
{

    [System.Serializable]
    public class GameState
    {
        public long startTime { get; set; } //���� ���� �ð�
        public PlayerData[] players { get; set; } = new PlayerData[2]; //�÷��̾� ������ �迭
    }

    public GameClient1 gameClient; //���� Ŭ���̾�Ʈ ��ũ��Ʈ ����
    public GameState gameState; //���� ���� ������
    public int myIndex = -1;

    public Text player1Data;
    public Text player2Data;
    // Start is called before the first frame update
    void Start()
    {
        gameState.players[0] = new PlayerData(); // �÷��̾� 1 �ʱ�ȭ
        gameState.players[1] = new PlayerData(); // �÷��̾� 2 �ʱ�ȭ
    }

    // Update is called once per frame
    void Update()
    {
        player1Data.text = gameState.players[0].ToString(); 
        player2Data.text = gameState.players[1].ToString();
    }

    public void UpdateGameState(string payload)
    {
        //���� ���� ������Ʈ ó��
        
        gameState = JsonConvert.DeserializeObject<GameState>(payload);
        if(myIndex == -1)
        {
            //�÷��̾� ID�� ���� �ε��� ����
            if(gameClient.PlayerId == gameState.players[0].id)
            {
                myIndex = 0;
            }
            else if(gameClient.PlayerId == gameState.players[1].id)
            {
                myIndex = 1;
            }
        }
    }
}
