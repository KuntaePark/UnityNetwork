using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataForm;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    //�� �÷��̾� �г�
    public Text username;
    
    //hp
    public Slider HPbar;
    public Text HpText;

    //mp
    public Slider MPbar;
    public Text MpText;

    //strengthLevel
    public Slider StrBar;

    public Image[] actionPanels = new Image[3]; // �׼� �г� �̹��� �迭 (����, ���, �����)
    public int selected = 0;

    public Text[] buttonTexts = new Text[2]; //Ű ��ư �ؽ�Ʈ

    // Start is called before the first frame update
    void Start()
    {
        HPbar.maxValue = 100; // �ִ� HP �� ����
        HPbar.value = 100;

        MPbar.maxValue = 10; // �ִ� MP �� ����
        MPbar.value = 0;

        StrBar.maxValue = 5;
        StrBar.value = 0;

    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < actionPanels.Length; i++)
        {
            if (i == selected)
            {
                actionPanels[i].color = Color.green; // ���õ� �׼� �г��� �����
            }
            else
            {
                actionPanels[i].color = Color.white; // ���õ��� ���� �׼� �г��� ���
            }
        }
    }

    public void showPlayerInfo(PlayerData player)
    {
        HPbar.value = player.hp; // �÷��̾� HP �����̴� �� ������Ʈ
        HpText.text = player.hp.ToString();
        MPbar.value = player.mp; // �÷��̾� MP �����̴� �� ������Ʈ
        MpText.text = player.mp.ToString();
        StrBar.value = player.strengthLevel;
    }

    public void setButtonText(bool isActionSelected)
    {
        if (isActionSelected)
        {
            buttonTexts[0].text = "����\n(Ctrl)"; // ���� �Ϸ� ��ư �ؽ�Ʈ
            buttonTexts[1].text = "���\n(Alt)"; // ��� ��ư �ؽ�Ʈ
        }
        else
        {
            buttonTexts[0].text = "����\n(Ctrl)"; // ���� ��ư �ؽ�Ʈ
            buttonTexts[1].text = "���� ����\n(Alt)"; // ���� ���� ��ư �ؽ�Ʈ
        }
    }
}
