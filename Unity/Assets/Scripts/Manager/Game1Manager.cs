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
        public float atk = 2f;
        public int strengthLevel = 0;
        public string currentAction = "attack";
        public string skillId = "heal";

        //�ܾ� ����
        public string currentWord = "";
        public string options = "";
        public int correctIdx = -1;

        //Ư��
        public float shieldRate = 0f;

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
