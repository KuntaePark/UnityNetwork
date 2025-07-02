using System.Collections;
using System.Collections.Generic;
using System;
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
        public string state = "ready";
        public PlayerData[] players { get; set; } = new PlayerData[2]; //�÷��̾� ������ �迭
    }

    public GameClient1 gameClient; //���� Ŭ���̾�Ʈ ��ũ��Ʈ ����
    public GameState gameState; //���� ���� ������
    public int myIdx = -1;

    public const int timeLimit = 99; //���� �ð� ����(��)

    public Text player1Data;
    public Text player2Data;
    public Text commonData;
    // Start is called before the first frame update
    void Start()
    {
        gameState.players[0] = new PlayerData(); // �÷��̾� 1 �ʱ�ȭ
        gameState.players[1] = new PlayerData(); // �÷��̾� 2 �ʱ�ȭ
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateGameState(string payload)
    {
        //���� ���� ������Ʈ ó��

        gameState = JsonConvert.DeserializeObject<GameState>(payload);
    }

    public bool checkActionSelected() { return gameState.players[myIdx].isActionSelected; }

    public long getTimesLeft()
    {
        if (gameState.startTime == 0)
        {
            //������ ���۵��� �ʾ����� 0 ��ȯ
            return 0;
        }
        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return timeLimit * 1000 + gameState.startTime - (long)(DateTime.UtcNow - epoch).TotalMilliseconds;
    }

    public void endGame(int winnerIdx)
    {
        //���� ���� ó��
        Debug.Log("Game ended. Winner index: " + winnerIdx);
        gameState.startTime = 0;
        gameState.state = "end";
        if (winnerIdx == myIdx)
        {
            Debug.Log("You win!");
        }
        else if(winnerIdx == 1 - myIdx)
        {
            Debug.Log("You lose!");
        }
        else
        {
            Debug.Log("It's a draw!");
        }

    }
}
