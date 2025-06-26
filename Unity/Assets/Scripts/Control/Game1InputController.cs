using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Game1InputController : MonoBehaviour
{
    public enum ActionType
    {
        ATTACK,
        DEFENSE,
        SPECIAL
    }



    //���� �Է��� �޾� ������ ����
    public GameClient1 gameClient;
    public ActionType actionType = ActionType.ATTACK;
    public bool isActionSelected = false;

    private float inputDelay = 0.2f; //�Է� ������ �ð�
    private float lastInputTime = 0f; //������ �Է� �ð�

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float axisH = Input.GetAxisRaw("Horizontal");
        float axisV = Input.GetAxisRaw("Vertical");

        //�Է� ó��, input.type ����: chargeMana, actionSelect, actionCancek, wordSelect 
        if (Input.GetButton("Fire2"))
        {
            //���� ����
            gameClient.Send("input",JsonConvert.SerializeObject(new { type = "chargeMana" }));
        }
        if(Time.time - lastInputTime < inputDelay)
        {
            //�Է� �����̰� �ɷ������� ����
            return;
        }
        if (!isActionSelected)
        { 
            if(axisH == -1.0f)
            {
                if(actionType != ActionType.ATTACK)
                {
                    Debug.Log("Action to left");
                    actionType--;
                    lastInputTime = Time.time; //�Է� �ð� ����
                }
            }
            if(axisH == 1.0f)
            {
                if(actionType != ActionType.SPECIAL)
                {
                    Debug.Log("Action to right");
                    actionType++;
                    lastInputTime = Time.time; //�Է� �ð� ����
                }
            }
            //�׼� ����
            if(Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Action selected: " + actionType.ToString());
                gameClient.Send("input", JsonConvert.SerializeObject(new { type = "actionSelect", action = actionType.ToString() }));
                isActionSelected = true; //���� ����
                lastInputTime = Time.time; //�Է� �ð� ����
            }
        }
        //�ܾ� �� ����
        else
        {
            //�ε��� ���������� �ð� �������� 0,1,2,3
            if(axisV == 1.0f)
            {
                //�� �ܾ� ����
                Debug.Log("Selecting word at index 0");
                gameClient.Send("input", JsonConvert.SerializeObject(new { type = "wordSelect", idx = 0 }));
                lastInputTime = Time.time; //�Է� �ð� ����
                isActionSelected = false; //���� ����
            }
            if(axisH == 1.0f)
            {
                //������ �ܾ� ����
                Debug.Log("Selecting word at index 1");
                gameClient.Send("input", JsonConvert.SerializeObject(new { type = "wordSelect", idx = 1 }));
                lastInputTime = Time.time; //�Է� �ð� ����
                isActionSelected = false; //���� ����
            }
            if(axisV == -1.0f)
            {
                //�Ʒ� �ܾ� ����
                Debug.Log("Selecting word at index 2");
                gameClient.Send("input", JsonConvert.SerializeObject(new { type = "wordSelect", idx = 2 }));
                lastInputTime = Time.time; //�Է� �ð� ����
                isActionSelected = false; //���� ����
            }
            if(axisH == -1.0f)
            {
                //���� �ܾ� ����
                Debug.Log("Selecting word at index 3");
                gameClient.Send("input", JsonConvert.SerializeObject(new { type = "wordSelect", idx = 3 }));
                lastInputTime = Time.time; //�Է� �ð� ����
                isActionSelected = false; //���� ����
            }
            //��� �Է� ���⿡
            if(Input.GetButtonDown("Fire1"))
            {
                //�׼� �������� ���ư���
                Debug.Log("Cancelling action selection");
                gameClient.Send("input", JsonConvert.SerializeObject(new { type = "actionCancel" }));
                isActionSelected = false; //���� ����
                lastInputTime = Time.time; //�Է� �ð� ����
            }
        }
    }
}
