using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1Manager : MonoBehaviour
{
    //UI ǥ�ø� ���� ������
    

    public class PlayerData
    {
        //�⺻ ��ġ
        public float hp = 100f;
        public float mp = 0f;
        public int strengthLevel = 0;
        public bool isActionSelected = false;

        //�ܾ� ����
        public string currentWord = "";
        public int[] options = new int[4];
        public int correctIdx = -1;
    }

    PlayerData[] players = new PlayerData[2]; // �÷��̾� 2�� ����

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
