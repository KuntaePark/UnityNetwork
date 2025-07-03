using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DataForm;
using Newtonsoft.Json;

public class GameStartUI : MonoBehaviour
{
    private BrowserRequest browserRequest;
    private int requestId;

    //UI 요소
    public Button gameStartButton;
    public Text scoreText;
    public Text rankingText;

    private void Awake()
    {
        browserRequest = new BrowserRequest();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameStartButton.onClick.AddListener(() =>
        {
            int requestId = browserRequest.StartRequest("POST", "/game/match/join");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadGameStartUI()
    {
        //서버에게 해당 유저의 정보 요청, 5초까지 기다림
        int requestId = browserRequest.StartRequest("GET", "/game/test");
        StartCoroutine(browserRequest.waitForResponse(requestId, 5.0f, (response) =>
        {
            if (response != null)
            {
                var userGameData = JsonConvert.DeserializeObject<UserGameData>(response.body);
                setGameStartUI(userGameData.game1Score, userGameData.ranking);
            }
            else
            {
                Debug.Log("정보 조회에 실패했습니다.");
            }

        }));
    }

    private void setGameStartUI(int score, long ranking)
    {
        scoreText.text = score.ToString();
        rankingText.text = ranking.ToString();
    }
}
