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



    //유저 입력을 받아 서버에 전달
    public GameClient1 gameClient;
    public ActionType actionType = ActionType.ATTACK;
    public bool isActionSelected = false;

    private float inputDelay = 0.2f; //입력 딜레이 시간
    private float lastInputTime = 0f; //마지막 입력 시간

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float axisH = Input.GetAxisRaw("Horizontal");
        float axisV = Input.GetAxisRaw("Vertical");

        //입력 처리, input.type 종류: chargeMana, actionSelect, actionCancek, wordSelect 
        if (Input.GetButton("Fire2"))
        {
            //마나 충전
            gameClient.Send("input",JsonConvert.SerializeObject(new { type = "chargeMana" }));
        }
        if(Time.time - lastInputTime < inputDelay)
        {
            //입력 딜레이가 걸려있으면 무시
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
                    lastInputTime = Time.time; //입력 시간 갱신
                }
            }
            if(axisH == 1.0f)
            {
                if(actionType != ActionType.SPECIAL)
                {
                    Debug.Log("Action to right");
                    actionType++;
                    lastInputTime = Time.time; //입력 시간 갱신
                }
            }
            //액션 결정
            if(Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Action selected: " + actionType.ToString());
                gameClient.Send("input", JsonConvert.SerializeObject(new { type = "actionSelect", action = actionType.ToString() }));
                isActionSelected = true; //상태 변경
                lastInputTime = Time.time; //입력 시간 갱신
            }
        }
        //단어 뜻 선택
        else
        {
            //인덱스 위에서부터 시계 방향으로 0,1,2,3
            if(axisV == 1.0f)
            {
                //위 단어 선택
                Debug.Log("Selecting word at index 0");
                gameClient.Send("input", JsonConvert.SerializeObject(new { type = "wordSelect", idx = 0 }));
                lastInputTime = Time.time; //입력 시간 갱신
                isActionSelected = false; //상태 변경
            }
            if(axisH == 1.0f)
            {
                //오른쪽 단어 선택
                Debug.Log("Selecting word at index 1");
                gameClient.Send("input", JsonConvert.SerializeObject(new { type = "wordSelect", idx = 1 }));
                lastInputTime = Time.time; //입력 시간 갱신
                isActionSelected = false; //상태 변경
            }
            if(axisV == -1.0f)
            {
                //아래 단어 선택
                Debug.Log("Selecting word at index 2");
                gameClient.Send("input", JsonConvert.SerializeObject(new { type = "wordSelect", idx = 2 }));
                lastInputTime = Time.time; //입력 시간 갱신
                isActionSelected = false; //상태 변경
            }
            if(axisH == -1.0f)
            {
                //왼쪽 단어 선택
                Debug.Log("Selecting word at index 3");
                gameClient.Send("input", JsonConvert.SerializeObject(new { type = "wordSelect", idx = 3 }));
                lastInputTime = Time.time; //입력 시간 갱신
                isActionSelected = false; //상태 변경
            }
            //취소 입력 여기에
            if(Input.GetButtonDown("Fire1"))
            {
                //액션 선택으로 돌아가기
                Debug.Log("Cancelling action selection");
                gameClient.Send("input", JsonConvert.SerializeObject(new { type = "actionCancel" }));
                isActionSelected = false; //상태 변경
                lastInputTime = Time.time; //입력 시간 갱신
            }
        }
    }
}
