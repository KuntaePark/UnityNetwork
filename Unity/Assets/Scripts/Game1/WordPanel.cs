using DataForm;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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

                Debug.Log($"{words[i].meaning}");
                wordMeanings[i].text = words[i].meaning;
            }
        }
    }

    public void deactivateOptions()
    {
        word.text = "행동을 선택하세요!";
        optionGroup.SetActive(false);
    }

    public void activateOptions()
    {
        optionGroup.SetActive(true);
    }
}

