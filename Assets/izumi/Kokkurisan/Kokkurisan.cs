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
    //[Header("�ς̑��̐����̕���")] // �Q�[���}�l�[�W���[�ֈړ�
    //[SerializeField]
    //private string _kituneClearStr;
    [Header("�ʏ펞�̉񓚂̕���")]
    public string normalAnswerStr;
    //[Header("�ʏ펞�̐����̕���")] // �Q�[���}�l�[�W���[�ֈړ�
    //[SerializeField]
    //private string _normalClearStr;


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

    [Header("�������肳��N���A")]
    //[System.NonSerialized]
    public bool isClear;

    [Header("�������肳��Е��N���A")]
    public bool isSideClear;

    [Header("����0(�N���A)�G�t�F�N�g")]
    public List<ParticleSystem> ef0 = new List<ParticleSystem>();

    [Header("�Е�0�G�t�F�N�g")]
    public List<ParticleSystem> ef0_1 = new List<ParticleSystem>();

    [Header("�ς̑�")]
    private Image frame;
    [Header("�ς̑��̒ʏ펞")]
    public Sprite frameNormal;
    [Header("�ς̑��̂��̂���������")]
    public Sprite frameFind;

    [Header("�Z���t��������_�l�̖�")]
    public string normalMaruStr;
    [Header("�Z���t��������_�ς̖�")]
    public string foxMaruStr;

    // �ԈႢ������
    // �l�̖�
    private int normalMissNum;
    // �ς̖�
    private int foxMissNum;
    public int NormalMissNum {
        get {
            return normalMissNum;
        }
    }
    public int FoxMissNum
    {
        get
        {
            return foxMissNum;
        }
    }

    private Canvas canvas;  // �L�����o�X�Ƀ��C���J������ݒ肷�邽�߂Ɏ擾

    // Start is called before the first frame update
    void Start()
    {
        markClear.gameObject.SetActive(false);
        canvas = GetComponent<Canvas>();

        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
        canvas.sortingLayerName = "TopLayer";
        canvas.sortingOrder = 2;    // ������ɕ\��

        frame = GameObject.FindGameObjectWithTag("Frame").GetComponent<Image>();
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

        // �N���A����///////////////////////////////////////
        if (normalAnswerStr != null && kituneAnswerStr != null && CPData.normalClearStr != null && CPData.kituneClearStr != null)
        {
            bool isNormalClear = ClearJudge(normalAnswerStr, CPData.normalClearStr, ref normalMissNum, false);
            bool isFoxClear = ClearJudge(kituneAnswerStr, CPData.kituneClearStr, ref foxMissNum, false);
            // �l�̖�,�ς̖ڗ����N���A�̏ꍇ�́A�������肳��N���A
            if (isNormalClear && isFoxClear)
            {
                isClear = true;
                isSideClear = false;
            }
            else
            {
                if (isNormal && isNormalClear)
                {
                    isSideClear = true;
                }
                else
                {
                    isSideClear = false;
                }

                if (isFox && isFoxClear)
                {
                    isSideClear = true;
                }
                else
                {
                    isSideClear = false;
                }

                isClear = false;
            }
        }
        else
        {
            isClear = false;
            isSideClear = false;
        }
        ///////////////////////////////////////////////////

        if (normalAnswerStr != null)
        {
            frame.sprite = frameFind;
        }
        else
        {
            frame.sprite = frameNormal;
        }

        // �񓚂��������̕\��
        if (isAnswer)
        {
            string answerStr = "";
            string clearStr = "";
            int missNum = 0;

            if (isNormal)
            {
                answerStr = normalAnswerStr;
                clearStr = CPData.normalClearStr;
                missNum = normalMissNum;
            }
            if (isFox)
            {
                answerStr = kituneAnswerStr;
                clearStr = CPData.kituneClearStr;
                missNum = foxMissNum;
            }


            if (answerStr != null && clearStr != null)
            {

                // ���ꂼ��̖ڂł̏���
                char[] answerChara = answerStr.ToCharArray();
                for (int i = 0; i < answerChara.Length; i++)
                {
                    //// �����̏ꍇ
                    //if ((int)answerChara[i] <= 57)
                    //{
                    //    int answerNo = answerChara[i] - 48;
                    //    if (numList[answerNo] != null)
                    //    {
                    //        numList[answerNo].enabled = true;
                    //        numList[answerNo].color = new Color(numList[answerNo].color.r, numList[answerNo].color.g, numList[answerNo].color.b, valueNowAnswer);
                    //    }
                    //}
                    //// �����̏ꍇ
                    //else
                    //{
                    int answerNo = (int)answerChara[i] - 12353;

                    if (charList[answerNo] != null)
                    {
                        charList[answerNo].enabled = true;
                        charList[answerNo].color = new Color(charList[answerNo].color.r, charList[answerNo].color.g, charList[answerNo].color.b, valueNowAnswer);
                    }
                    //}
                }
                // �������������̏ꍇ�́Z�ŕ������͂�
                if (markObjList.Count == 0)
                {
                    ClearJudge(answerStr, clearStr, ref missNum, true);

                    // �Z���͂ގ�(�������肳��̕\�������u��,�ڂ̐؂�ւ����u��)�ɃN���A��������A�G�t�F�N�g���Đ�
                    if (isClear)
                    {
                        foreach(ParticleSystem ef in ef0)
                        {
                            ef.Play();
                        }
                    }
                    if (isSideClear)
                    {
                        foreach (ParticleSystem ef in ef0_1)
                        {
                            ef.Play();
                        }
                    }
                }


                // �ԈႦ����������\��
                if (numList[missNum] != null)
                {
                    numList[missNum].enabled = true;
                    numList[missNum].color = new Color(numList[missNum].color.r, numList[missNum].color.g, numList[missNum].color.b, valueNowAnswer);
                }
            }
            // �񓚂����������������ĂȂ��ꍇ(�G���[)
            else
            {
                missNum = -1;
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

        // �G�t�F�N�g�N���A
        foreach (ParticleSystem ef in ef0)
        {
            ef.Clear();
        }
        foreach (ParticleSystem ef in ef0_1)
        {
            ef.Clear();
        }
    }

    // �N���A����(isMarkPrint��true���Ɛ��𕶎����Z�ň͂�)
    bool ClearJudge(string answerStr, string clearStr, ref int missNum, bool isMarkPrint)
    {

        char[] clearChara = clearStr.ToCharArray();
        missNum = 0;
        for (int i = 0; i < clearChara.Length; i++)
        {
            bool clear = false;
            // �񓚂���������̒��Ő����̕������܂�ł����ꍇ
            if (answerStr.Contains(clearChara[i]))
            {
                clear = true;
            }

            // ������������(�������܂ނ�)
            if (clear)
            {
                // isMarkPrint��true�̏ꍇ�́Z�ň͂�
                if (isMarkPrint)
                {
                    int clearNo = (int)clearChara[i] - 12353;
                    GameObject markObj = Instantiate(markClear.gameObject, charList[clearNo].transform);
                    markObj.transform.localPosition = new Vector3(-100, -25, 0);
                    markObj.SetActive(true);
                    markObjList.Add(markObj);
                    // �Z�ň͂܂ꂽ������maruStr�Ɋi�[
                    if (isNormal)
                    {
                        if (!normalMaruStr.Contains(clearChara[i]))
                        {
                            normalMaruStr = normalMaruStr + clearChara[i];
                        }
                    }
                    if (isFox)
                    {
                        if (!foxMaruStr.Contains(clearChara[i]))
                        {
                            foxMaruStr = foxMaruStr + clearChara[i];
                        }
                    }
                }
            }
            // �s������������(�������܂�łȂ�����)
            else
            {
                // �ԈႢ��������ǉ�
                missNum++;
            }
        }

        // �ԈႢ��������0�̏ꍇ�̓N���A
        if (missNum == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}


