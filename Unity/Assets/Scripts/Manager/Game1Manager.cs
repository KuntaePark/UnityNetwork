using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1Manager : MonoBehaviour
{
    //UI 표시를 위한 데이터
    

    public class PlayerData
    {
        //기본 수치
        public float hp = 100f;
        public float mp = 0f;
        public int strengthLevel = 0;
        public bool isActionSelected = false;

        //단어 관련
        public string currentWord = "";
        public int[] options = new int[4];
        public int correctIdx = -1;
    }

    PlayerData[] players = new PlayerData[2]; // 플레이어 2명 관리

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
