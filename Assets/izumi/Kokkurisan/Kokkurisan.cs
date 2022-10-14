//=============================================================================
//
// �������肳�񏈗�
//
// �쐬��:2022/10/13
// �쐬��:��D��
//
// <�J������>
// 2022/10/13 �쐬
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Kokkurisan : MonoBehaviour
{
    [Header("Yes��\�����邩")]
    public bool isYes;
    [Header("No��\�����邩")]
    public bool isNo;
    [Header("������\�����邩")]
    public bool isAnswer;

    [Header("�\�����铚��")]
    public string answerStr;

    [Header("�\�����x")]
    public float answerSpeed = 0.5f;
    // ���݂̕\�����Ă���l
    private float valueNowAnswer = 0;

    [Header("�\���������X�g")]
    public List<TextMeshProUGUI> charList;
    [Header("�\���������X�g")]
    public List<TextMeshProUGUI> numList;
    [Header("�\���͂�")]
    public TextMeshProUGUI yesText;
    [Header("�\��������")]
    public TextMeshProUGUI noText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �\�����x
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


        // ������\������
        if (isAnswer)
        {
            char[] answerChara = answerStr.ToCharArray();
            for (int i = 0; i < answerChara.Length; i++)
            {
                // �����̏ꍇ
                if ((int)answerChara[i] <= 57)
                {
                    int answerNo = answerChara[i] - 48;
                    if (numList[answerNo] != null)
                    {
                        numList[answerNo].enabled = true;
                        numList[answerNo].color = new Color(numList[answerNo].color.r, numList[answerNo].color.g, numList[answerNo].color.b, valueNowAnswer);
                    }
                }
                // �����̏ꍇ
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
        //�@������\�����Ȃ�
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

        // �͂�
        if (isYes)
        {
            yesText.enabled = true;
            yesText.color = new Color(yesText.color.r, yesText.color.g, yesText.color.b, valueNowAnswer);
        }
        else
        {
            yesText.enabled = false;
        }

        // ������
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
