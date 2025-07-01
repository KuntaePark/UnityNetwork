using DataForm;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class WordPanel : MonoBehaviour
{
    public Text[] wordMeanings;
    public Text word;

    public GameObject optionGroup;
    // Start is called before the first frame update
    void Start()
    {
        deactivateOptions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showWord(PlayerData myInfo)
    {
        int correctIdx = myInfo.correctIdx;
        if (correctIdx != -1)
        {
            wordData[] words = myInfo.words;
            word.text = words[correctIdx].word_text;
            for (int i = 0; i < 4; i++)
            {
                wordMeanings[i].text = words[i].meaning;
            }
        }
    }

    public void deactivateOptions()
    {
        word.text = "�ൿ�� �����ϼ���!";
        optionGroup.SetActive(false);
    }

    public void activateOptions()
    {
        optionGroup.SetActive(true);
    }
}

