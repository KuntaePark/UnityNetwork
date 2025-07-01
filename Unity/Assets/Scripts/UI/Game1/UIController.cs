using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataForm;

public class UIController : MonoBehaviour
{

    public Game1Manager game1Manager; //���� �Ŵ��� ��ũ��Ʈ ����

    public PlayerPanel[] playerPanels = new PlayerPanel[2]; //�÷��̾� �г� �迭
    public WordPanel wordPanel; //�ܾ� �г�

    //�ð�
    public Slider TimeBar;
    public Text TimeText;

    // Start is called before the first frame update
    void Start()
    {
        TimeBar.maxValue = Game1Manager.timeLimit * 1000; //�����̴� �ִ밪 ����
        TimeBar.value = Game1Manager.timeLimit * 1000; //�����̴� �ʱⰪ ����
    }

    // Update is called once per frame
    void Update()
    {

        if (game1Manager.gameState.state == "start")
        {
            var players = game1Manager.gameState.players;
            for(int i = 0; i < 2; i++)
            {
                playerPanels[i].showPlayerInfo(players[i]);
                if(i != game1Manager.myIdx)
                {
                    //������ �׼� ������ ������ ����ȭ
                    string action = players[i].currentAction;
                    switch(action)
                    {
                        case "ATTACK":
                            playerPanels[i].selected = 0; //����
                            break;
                        case "DEFENSE":
                            playerPanels[i].selected = 1; //���
                            break;
                        case "SPECIAL":
                            playerPanels[i].selected = 2; //�����
                            break;
                        default:
                            playerPanels[i].selected = -1; //���� ����
                            break;
                    }
                }
            }

            TimeBar.value = game1Manager.getTimesLeft();
            TimeText.text = $"{game1Manager.getTimesLeft() / 1000}";

            //�ܾ� ���� UI
            var myInfo = players[game1Manager.myIdx];
            if(myInfo.isActionSelected)
            {
                wordPanel.activateOptions();
                wordPanel.showWord(myInfo);
                playerPanels[game1Manager.myIdx].setButtonText(true);
            }
            else
            {
                wordPanel.deactivateOptions();
                playerPanels[game1Manager.myIdx].setButtonText(false);
            }
        }
    }

    public void setAction(int selected)
    {
        playerPanels[game1Manager.myIdx].selected = selected;
    }
}
