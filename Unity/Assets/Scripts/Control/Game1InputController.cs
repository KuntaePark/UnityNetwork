using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class Game1InputController : MonoBehaviour
{
    public enum ActionType
    {
        ATTACK,
        DEFENSE,
        SPECIAL
    }


    public Game1Manager game1Manager; //���� �Ŵ��� ��ũ��Ʈ ����
    public UIController uiController;

    //���� �Է��� �޾� ������ ����
    public GameClient1 gameClient;
    public ActionType actionType = ActionType.ATTACK;
   

    private float inputDelay = 0.2f; //�Է� ������ �ð�
    private float lastInputTime = 0f; //������ �Է� �ð�

    //���� �ε��� ����
    Dictionary<Vector2, int> directionIndexMap = new Dictionary<Vector2, int>
    {
        { new Vector2(0, 1), 0 },   // ��
        { new Vector2(1, 0), 1 },   // ������
        { new Vector2(0, -1), 2 },  // �Ʒ�
        { new Vector2(-1, 0), 3 }   // ����
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(game1Manager.gameState.state != "start")
        {
            //������ ���۵��� �ʾ����� �Է� ����
            return;
        }

        if(Time.time - lastInputTime < inputDelay)
        {
            //�Է� �����̰� �ɷ������� ����
            return;
        }

        float axisH = Input.GetAxisRaw("Horizontal");
        float axisV = Input.GetAxisRaw("Vertical");
       
        bool isActionSelected = game1Manager.checkActionSelected(); //�׼� ���� ����

        //�Է� ó��, input.type ����: chargeMana, actionSelect, actionCancel, wordSelect 
        if (!isActionSelected)
        {
            //�ൿ �̼��� ���¿����� �Է� ó��
            if (Input.GetButton("Fire2"))
            {
                //���� ����
                gameClient.Send("input", JsonConvert.SerializeObject(new { type = "chargeMana" }));
            }
            if (axisH == -1.0f)
            {
                if (actionType != ActionType.ATTACK)
                {
                    Debug.Log("Action to left");
                    actionType--;
                    uiController.setAction((int)actionType);
                    lastInputTime = Time.time; //�Է� �ð� ����
                }
            }
            if (axisH == 1.0f)
            {
                if (actionType != ActionType.SPECIAL)
                {
                    Debug.Log("Action to right");
                    actionType++;
                    uiController.setAction((int)actionType);
                    lastInputTime = Time.time; //�Է� �ð� ����
                }
            }
            //�׼� ����
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Action selected: " + actionType.ToString());
                gameClient.Send("input", JsonConvert.SerializeObject(new { type = "actionSelect", action = actionType.ToString() }));
                isActionSelected = true; //���� ����
                lastInputTime = Time.time; //�Է� �ð� ����
            }
        }
        else
        {
            //�ൿ ���� ���¿����� �Է� ó��
            if (Input.GetButtonDown("Fire1"))
            {
                //�ൿ ����
                Debug.Log("Action confirm");
                gameClient.Send("input", JsonConvert.SerializeObject(new { type = "actionConfirm" }));
                lastInputTime = Time.time; //�Է� �ð� ����
            }

            //�ൿ ���
            if (Input.GetButtonDown("Fire2"))
            {
                Debug.Log("Action cancelled");
                gameClient.Send("input", JsonConvert.SerializeObject(new { type = "actionCancel" }));
                isActionSelected = false; //���� ����
                lastInputTime = Time.time; //�Է� �ð� ����
            }

            //�ܾ� �� ����
            if(directionIndexMap.TryGetValue(new Vector2(axisH, axisV), out int index))
            {
                //���� �Է¿� ���� �ܾ� ����
                Debug.Log("Selecting word at index: " + index);
                gameClient.Send("input", JsonConvert.SerializeObject(new { type = "wordSelect", idx = index }));
                lastInputTime = Time.time; //�Է� �ð� ����
            }
        }
    }
}
