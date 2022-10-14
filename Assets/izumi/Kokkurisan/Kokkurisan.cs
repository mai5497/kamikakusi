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
using UnityEngine.UI;

public class Kokkurisan : MonoBehaviour
{
    [Header("人の目を表示するか")]
    public bool isNormal;
    [Header("狐の目を表示するか")]
    public bool isFox;

    [Header("答えを表示するか")]
    public bool isAnswer;

    [Header("回答の文字")]
    public string answerStr;
    [Header("正解の文字")]
    public string clearStr;

    [Header("表示速度")]
    public float answerSpeed = 0.5f;
    // 現在の表示している値
    private float valueNowAnswer = 0;

    [Header("表示文字リスト")]
    public List<TextMeshProUGUI> charList;
    [Header("表示数字リスト")]
    public List<TextMeshProUGUI> numList;
    [Header("正解した場合に表示する丸")]
    public Image markClear;
    private List<GameObject> markObjList = new List<GameObject>();
    [Header("表示人の目")]
    public Image normalYesImage;
    [Header("表示狐の目")]
    public Image foxYesImage;
    [Header("非表示人の目")]
    public Image normalNoImage;
    [Header("非表示狐の目")]
    public Image foxNoImage;

    // Start is called before the first frame update
    void Start()
    {
        markClear.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 表示速度
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


        // 回答した文字の表示
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
            // 正解した文字の場合は〇で文字を囲む
            if (markObjList.Count == 0)
            {
                char[] clearChara = clearStr.ToCharArray();
                for (int i = 0; i < clearChara.Length; i++)
                {
                    // 回答した文字列の中で正解の文字を含んでいた場合
                    if (answerStr.Contains(clearChara[i]))
                    {
                        int clearNo = (int)clearChara[i] - 12354;
                        // あ行
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
                        // か行
                        else if (clearNo < 18)
                        {
                            height = clearNo - 9;
                            width = 1;
                            GameObject markObj = Instantiate(markClear.gameObject, this.transform);
                            markObj.transform.localPosition = new Vector3(440 - width * 100, 110 - (height / 2) * 75, markClear.transform.position.z);
                            markObj.SetActive(true);
                            markObjList.Add(markObj);
                        }
                        // さ行
                        else if (clearNo < 27)
                        {
                            height = clearNo - 18;
                            width = 2;
                            GameObject markObj = Instantiate(markClear.gameObject, this.transform);
                            markObj.transform.localPosition = new Vector3(440 - width * 100, 110 - (height / 2) * 75, markClear.transform.position.z);
                            markObj.SetActive(true);
                            markObjList.Add(markObj);
                        }
                        // た行
                        else if (clearNo < 36)
                        {
                            height = clearNo - 27;
                            width = 3;
                            GameObject markObj = Instantiate(markClear.gameObject, this.transform);
                            markObj.transform.localPosition = new Vector3(440 - width * 100, 110 - (height / 2) * 75, markClear.transform.position.z);
                            markObj.SetActive(true);
                            markObjList.Add(markObj);
                        }
                        // な行
                        else if (clearNo < 45)
                        {
                            height = clearNo - 36;
                            width = 4;
                            GameObject markObj = Instantiate(markClear.gameObject, this.transform);
                            markObj.transform.localPosition = new Vector3(440 - width * 100, 110 - (height / 2) * 75, markClear.transform.position.z);
                            markObj.SetActive(true);
                            markObjList.Add(markObj);
                        }
                        // は行
                        else if (clearNo < 54)
                        {
                            height = clearNo - 45;
                            width = 5;
                            GameObject markObj = Instantiate(markClear.gameObject, this.transform);
                            markObj.transform.localPosition = new Vector3(440 - width * 100, 110 - (height / 2) * 75, markClear.transform.position.z);
                            markObj.SetActive(true);
                            markObjList.Add(markObj);
                        }
                        // ま行
                        else if (clearNo < 63)
                        {
                            height = clearNo - 54;
                            width = 6;
                            GameObject markObj = Instantiate(markClear.gameObject, this.transform);
                            markObj.transform.localPosition = new Vector3(440 - width * 100, 110 - (height / 2) * 75, markClear.transform.position.z);
                            markObj.SetActive(true);
                            markObjList.Add(markObj);
                        }
                        // や行
                        else if (clearNo < 68)
                        {
                            height = clearNo - 63;
                            width = 7;
                            GameObject markObj = Instantiate(markClear.gameObject, this.transform);
                            markObj.transform.localPosition = new Vector3(440 - width * 100, 110 - (height / 2) * 75, markClear.transform.position.z);
                            markObj.SetActive(true);
                            markObjList.Add(markObj);
                        }
                        // ら行
                        else if (clearNo < 77)
                        {
                            height = clearNo - 68;
                            width = 8;
                            GameObject markObj = Instantiate(markClear.gameObject, this.transform);
                            markObj.transform.localPosition = new Vector3(440 - width * 100, 110 - (height / 2) * 75, markClear.transform.position.z);
                            markObj.SetActive(true);
                            markObjList.Add(markObj);
                        }
                        // わ行
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

            //マークを全消し
            if (markObjList.Count != 0)
            {
                for (int i = 0; i < markObjList.Count; i++)
                {
                    Destroy(markObjList[i]);
                }
                markObjList.Clear();
            }
        }

        // 人の目
        if (isNormal)
        {
            normalNoImage.enabled = false;
            foxYesImage.enabled = false;
            normalYesImage.enabled = true;
            foxNoImage.enabled = true;
        }
        // 狐の目
        else if (isFox)
        {
            normalYesImage.enabled = false;
            foxNoImage.enabled = false;
            foxYesImage.enabled = true;
            normalNoImage.enabled = true;
        }
        // どちらでもない
        else
        {
            normalYesImage.enabled = false;
            foxYesImage.enabled = false;
            normalNoImage.enabled = true;
            foxNoImage.enabled = true;
        }
    }
}
