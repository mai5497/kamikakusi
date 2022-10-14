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

    [Header("������\�����邩")]
    public bool isAnswer;

    [Header("�񓚂̕���")]
    public string answerStr;
    [Header("�����̕���")]
    public string clearStr;

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
        // �\�����x
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
                        // ���s
                        int width = 0;
                        int height = 0;
                        if (clearNo < 9)
                        {
                            height = clearNo;
                            width = 0;
                            GameObject markObj = Instantiate(markClear.gameObject, this.transform);
                            markObj.transform.localPosition = new Vector3(440 - width * 100, 110 - (height / 2) * 75, markClear.transform.position.z);
                            markObj.SetActive(true);
                            markObjList.Add(markObj);
                        }
                        // ���s
                        else if (clearNo < 18)
                        {
                            height = clearNo - 9;
                            width = 1;
                            GameObject markObj = Instantiate(markClear.gameObject, this.transform);
                            markObj.transform.localPosition = new Vector3(440 - width * 100, 110 - (height / 2) * 75, markClear.transform.position.z);
                            markObj.SetActive(true);
                            markObjList.Add(markObj);
                        }
                        // ���s
                        else if (clearNo < 27)
                        {
                            height = clearNo - 18;
                            width = 2;
                            GameObject markObj = Instantiate(markClear.gameObject, this.transform);
                            markObj.transform.localPosition = new Vector3(440 - width * 100, 110 - (height / 2) * 75, markClear.transform.position.z);
                            markObj.SetActive(true);
                            markObjList.Add(markObj);
                        }
                        // ���s
                        else if (clearNo < 36)
                        {
                            height = clearNo - 27;
                            width = 3;
                            GameObject markObj = Instantiate(markClear.gameObject, this.transform);
                            markObj.transform.localPosition = new Vector3(440 - width * 100, 110 - (height / 2) * 75, markClear.transform.position.z);
                            markObj.SetActive(true);
                            markObjList.Add(markObj);
                        }
                        // �ȍs
                        else if (clearNo < 45)
                        {
                            height = clearNo - 36;
                            width = 4;
                            GameObject markObj = Instantiate(markClear.gameObject, this.transform);
                            markObj.transform.localPosition = new Vector3(440 - width * 100, 110 - (height / 2) * 75, markClear.transform.position.z);
                            markObj.SetActive(true);
                            markObjList.Add(markObj);
                        }
                        // �͍s
                        else if (clearNo < 54)
                        {
                            height = clearNo - 45;
                            width = 5;
                            GameObject markObj = Instantiate(markClear.gameObject, this.transform);
                            markObj.transform.localPosition = new Vector3(440 - width * 100, 110 - (height / 2) * 75, markClear.transform.position.z);
                            markObj.SetActive(true);
                            markObjList.Add(markObj);
                        }
                        // �܍s
                        else if (clearNo < 63)
                        {
                            height = clearNo - 54;
                            width = 6;
                            GameObject markObj = Instantiate(markClear.gameObject, this.transform);
                            markObj.transform.localPosition = new Vector3(440 - width * 100, 110 - (height / 2) * 75, markClear.transform.position.z);
                            markObj.SetActive(true);
                            markObjList.Add(markObj);
                        }
                        // ��s
                        else if (clearNo < 68)
                        {
                            height = clearNo - 63;
                            width = 7;
                            GameObject markObj = Instantiate(markClear.gameObject, this.transform);
                            markObj.transform.localPosition = new Vector3(440 - width * 100, 110 - (height / 2) * 75, markClear.transform.position.z);
                            markObj.SetActive(true);
                            markObjList.Add(markObj);
                        }
                        // ��s
                        else if (clearNo < 77)
                        {
                            height = clearNo - 68;
                            width = 8;
                            GameObject markObj = Instantiate(markClear.gameObject, this.transform);
                            markObj.transform.localPosition = new Vector3(440 - width * 100, 110 - (height / 2) * 75, markClear.transform.position.z);
                            markObj.SetActive(true);
                            markObjList.Add(markObj);
                        }
                        // ��s
                        else if (clearNo < 82)
                        {
                            height = clearNo - 77;
                            width = 9;
                            GameObject markObj = Instantiate(markClear.gameObject, this.transform);
                            markObj.transform.localPosition = new Vector3(440 - width * 100, 110 - (height / 2) * 75, markClear.transform.position.z);
                            markObj.SetActive(true);
                            markObjList.Add(markObj);
                        }
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

            //�}�[�N��S����
            if (markObjList.Count != 0)
            {
                for (int i = 0; i < markObjList.Count; i++)
                {
                    Destroy(markObjList[i]);
                }
                markObjList.Clear();
            }
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
    }
}
