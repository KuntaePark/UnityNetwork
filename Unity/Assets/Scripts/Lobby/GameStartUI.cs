using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class GameStartUI : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadGameStartUI()
    {
        //서버에게 해당 유저의 정보 요청
        UnityWebRequest webRequest = UnityWebRequest.Get("https://localhost/game/userGameData");
        if(webRequest.result == UnityWebRequest.Result.Success)
        {
            //success
        }
        else
        {
            //error
        }
    }
}
