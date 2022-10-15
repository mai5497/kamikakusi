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
using UnityEngine.UI;

public class Kokkurisan : MonoBehaviour
{
    [Header("�l�̖ڂ�\�����邩")]
    public bool isNormal;
    [Header("�ς̖ڂ�\�����邩")]
    public bool isFox;

    bool isNormalBefore;
    bool isFoxBefore;

    [Header("������\�����邩")]
    public bool isAnswer;

    [Header("�ς̑��̉񓚂̕���")]
    public string kituneAnswerStr;
    [Header("�ς̑��̐����̕���")]
    //[System.NonSerialized]
    public string kituneClearStr;
    [Header("�ʏ펞�̉񓚂̕���")]
    //[System.NonSerialized]
    public string normalAnswerStr;
    [Header("�ʏ펞�̐����̕���")]
    public string normalClearStr;


    [Header("�\�����x")]
    public float answerSpeed = 0.5f;
    // ���݂̕\�����Ă���l
    private float valueNowAnswer = 0;

    [Header("�\���������X�g")]
    public List<TextMeshProUGUI> charList;
    [Header("�\���������X�g")]
    public List<TextMeshProUGUI> numList;
    [Header("���������ꍇ�ɕ\�������")]
    public Image markClear;
    private List<GameObject> markObjList = new List<GameObject>();
    [Header("�\���l�̖�")]
    public Image normalYesImage;
    [Header("�\���ς̖�")]
    public Image foxYesImage;
    [Header("��\���l�̖�")]
    public Image normalNoImage;
    [Header("��\���ς̖�")]
    public Image foxNoImage;

    // Start is called before the first frame update
    void Start()
    {
        markClear.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // ���[�h�ύX�����Z�b�g
        if (isNormal != isNormalBefore)
        {
            ResetParam();
        }
        if (isFox != isFoxBefore)
        {
            ResetParam();
        }

        // �����̃��l�̌���
        if (isAnswer || isNormal || isFox)
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


        // �񓚂��������̕\��
        if (isAnswer)
        {
            string answerStr = "";
            string clearStr = "";
            if (isNormal)
            {
                answerStr = normalAnswerStr;
                clearStr = normalClearStr;
            }
            if (isFox)
            {
                answerStr = kituneAnswerStr;
                clearStr = kituneClearStr;
            }

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
            // �������������̏ꍇ�́Z�ŕ������͂�
            if (markObjList.Count == 0)
            {
                char[] clearChara = clearStr.ToCharArray();
                for (int i = 0; i < clearChara.Length; i++)
                {
                    // �񓚂���������̒��Ő����̕������܂�ł����ꍇ
                    if (answerStr.Contains(clearChara[i]))
                    {
                        int clearNo = (int)clearChara[i] - 12354;
                        GameObject markObj = Instantiate(markClear.gameObject, charList[clearNo].transform);
                        markObj.transform.localPosition = new Vector3(-100, -25, 0);
                        markObj.SetActive(true);
                        markObjList.Add(markObj);
                    }
                }
            }

        }
        //�@������\�����Ȃ�
        else
        {
            ResetParam();
        }

        // �l�̖�
        if (isNormal)
        {
            normalNoImage.enabled = false;
            foxYesImage.enabled = false;
            normalYesImage.enabled = true;
            foxNoImage.enabled = true;
        }
        // �ς̖�
        else if (isFox)
        {
            normalYesImage.enabled = false;
            foxNoImage.enabled = false;
            foxYesImage.enabled = true;
            normalNoImage.enabled = true;
        }
        // �ǂ���ł��Ȃ�
        else
        {
            normalYesImage.enabled = false;
            foxYesImage.enabled = false;
            normalNoImage.enabled = true;
            foxNoImage.enabled = true;
        }

        isNormalBefore = isNormal;
        isFoxBefore = isFox;
    }

    // ���[�h�J�ڎ��̃��Z�b�g
    void ResetParam()
    {
        // �ԕ����̔�\��
        for (int i = 0; i < charList.Count; i++)
        {
            if (charList[i] != null)
            {
                charList[i].enabled = false;
            }
        }
        for (int i = 0; i < numList.Count; i++)
        {
            if (numList[i] != null)
            {
                numList[i].enabled = false;
            }
        }

        //�}�[�N��S����
        if (markObjList.Count != 0)
        {
            for (int i = 0; i < markObjList.Count; i++)
            {
                if (markObjList[i] != null)
                {
                    Destroy(markObjList[i]);
                }
            }
            markObjList.Clear();
        }
    }
}


