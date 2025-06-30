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
        public long startTime { get; set; } //게임 시작 시간
        public PlayerData[] players { get; set; } = new PlayerData[2]; //플레이어 데이터 배열
    }

    public GameClient1 gameClient; //게임 클라이언트 스크립트 참조
    public GameState gameState; //게임 상태 데이터
    public int myIndex = -1;

    public Text player1Data;
    public Text player2Data;
    // Start is called before the first frame update
    void Start()
    {
        gameState.players[0] = new PlayerData(); // 플레이어 1 초기화
        gameState.players[1] = new PlayerData(); // 플레이어 2 초기화
    }

    // Update is called once per frame
    void Update()
    {
        player1Data.text = gameState.players[0].ToString(); 
        player2Data.text = gameState.players[1].ToString();
    }

    public void UpdateGameState(string payload)
    {
        //게임 상태 업데이트 처리
        
        gameState = JsonConvert.DeserializeObject<GameState>(payload);
        if(myIndex == -1)
        {
            //플레이어 ID를 통해 인덱스 설정
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
