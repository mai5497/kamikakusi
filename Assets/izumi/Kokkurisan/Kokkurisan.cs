//=============================================================================
//
// こっくりさん処理
//
// 作成日:2022/10/13
// 作成者:泉優樹
//
// <開発履歴>
// 2022/10/13 作成
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Kokkurisan : MonoBehaviour
{
    [Header("Yesを表示するか")]
    public bool isYes;
    [Header("Noを表示するか")]
    public bool isNo;
    [Header("答えを表示するか")]
    public bool isAnswer;

    [Header("表示する答え")]
    public string answerStr;

    [Header("表示速度")]
    public float answerSpeed = 0.5f;
    // 現在の表示している値
    private float valueNowAnswer = 0;

    [Header("表示文字リスト")]
    public List<TextMeshProUGUI> charList;
    [Header("表示数字リスト")]
    public List<TextMeshProUGUI> numList;
    [Header("表示はい")]
    public TextMeshProUGUI yesText;
    [Header("表示いいえ")]
    public TextMeshProUGUI noText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 表示速度
        if (isAnswer || isYes || isNo)
        {
            valueNowAnswer += answerSpeed * Time.deltaTime;
            if (valueNowAnswer > 1.0f)
            {
                valueNowAnswer = 1.0f;
            }
        }
        else
        {
            valueNowAnswer = 0;
        }


        // 答えを表示する
        if (isAnswer)
        {
            char[] answerChara = answerStr.ToCharArray();
            for (int i = 0; i < answerChara.Length; i++)
            {
                // 数字の場合
                if ((int)answerChara[i] <= 57)
                {
                    int answerNo = answerChara[i] - 48;
                    if (numList[answerNo] != null)
                    {
                        numList[answerNo].enabled = true;
                        numList[answerNo].color = new Color(numList[answerNo].color.r, numList[answerNo].color.g, numList[answerNo].color.b, valueNowAnswer);
                    }
                }
                // 文字の場合
                else
                {
                    int answerNo = (int)answerChara[i] - 12354;

                    if (charList[answerNo] != null)
                    {
                        charList[answerNo].enabled = true;
                        charList[answerNo].color = new Color(charList[answerNo].color.r, charList[answerNo].color.g, charList[answerNo].color.b, valueNowAnswer);
                    }
                }
            }
        }
        //　答えを表示しない
        else
        {

            for (int i = 0; i < charList.Count; i++)
            {
                if (charList[i] != null)
                {
                    charList[i].enabled = false;
                }
            }
            for(int i = 0; i < numList.Count; i++)
            {
                if (numList[i] != null)
                {
                    numList[i].enabled = false;
                }
            }
        }

        // はい
        if (isYes)
        {
            yesText.enabled = true;
            yesText.color = new Color(yesText.color.r, yesText.color.g, yesText.color.b, valueNowAnswer);
        }
        else
        {
            yesText.enabled = false;
        }

        // いいえ
        if (isNo)
        {
            noText.enabled = true;
            noText.color = new Color(noText.color.r, noText.color.g, noText.color.b, valueNowAnswer);
        }
        else
        {
            noText.enabled = false;
        }
    }
}
