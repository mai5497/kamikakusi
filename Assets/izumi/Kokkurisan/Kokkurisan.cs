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

    bool isNormalBefore;
    bool isFoxBefore;

    [Header("答えを表示するか")]
    public bool isAnswer;

    [Header("狐の窓の回答の文字")]
    public string kituneAnswerStr;
    //[Header("狐の窓の正解の文字")] // ゲームマネージャーへ移動
    //[SerializeField]
    //private string _kituneClearStr;
    [Header("通常時の回答の文字")]
    public string normalAnswerStr;
    //[Header("通常時の正解の文字")] // ゲームマネージャーへ移動
    //[SerializeField]
    //private string _normalClearStr;


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

    [Header("こっくりさんクリア")]
    //[System.NonSerialized]
    public bool isClear;

    // 間違い文字数
    // 人の目
    private int normalMissNum;
    // 狐の目
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

    // Start is called before the first frame update
    void Start()
    {
        markClear.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        // モード変更時リセット
        if (isNormal != isNormalBefore)
        {
            ResetParam();
        }
        if (isFox != isFoxBefore)
        {
            ResetParam();
        }

        // 文字のα値の決定
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

        // クリア判定///////////////////////////////////////
        if (normalAnswerStr != null && kituneAnswerStr != null && CPData.normalClearStr != null && CPData.kituneClearStr != null)
        {
            bool isNormalClear = ClearJudge(normalAnswerStr, CPData.normalClearStr, ref normalMissNum, false);
            bool isFoxClear = ClearJudge(kituneAnswerStr, CPData.kituneClearStr, ref foxMissNum, false);
            // 人の目,狐の目両方クリアの場合は、こっくりさんクリア
            if (isNormalClear && isFoxClear)
            {
                isClear = true;
            }
            else
            {
                isClear = false;
            }
        }
        else
        {
            isClear = false;
        }
        ///////////////////////////////////////////////////

        // 回答した文字の表示
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

                // それぞれの目での処理
                char[] answerChara = answerStr.ToCharArray();
                for (int i = 0; i < answerChara.Length; i++)
                {
                    //// 数字の場合
                    //if ((int)answerChara[i] <= 57)
                    //{
                    //    int answerNo = answerChara[i] - 48;
                    //    if (numList[answerNo] != null)
                    //    {
                    //        numList[answerNo].enabled = true;
                    //        numList[answerNo].color = new Color(numList[answerNo].color.r, numList[answerNo].color.g, numList[answerNo].color.b, valueNowAnswer);
                    //    }
                    //}
                    //// 文字の場合
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
                // 正解した文字の場合は〇で文字を囲む
                if (markObjList.Count == 0)
                {
                    ClearJudge(answerStr, clearStr, ref missNum, true);
                }


                // 間違えた文字数を表示
                if (numList[missNum] != null)
                {
                    numList[missNum].enabled = true;
                    numList[missNum].color = new Color(numList[missNum].color.r, numList[missNum].color.g, numList[missNum].color.b, valueNowAnswer);
                }
            }
            // 回答も正解も何も入ってない場合(エラー)
            else
            {
                missNum = -1;
            }

        }
        //　答えを表示しない
        else
        {
            ResetParam();
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

        isNormalBefore = isNormal;
        isFoxBefore = isFox;
    }

    // モード遷移時のリセット
    void ResetParam()
    {
        // 赤文字の非表示
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

        //マークを全消し
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

    // クリア判定(isMarkPrintがtrueだと正解文字を〇で囲む)
    bool ClearJudge(string answerStr, string clearStr, ref int missNum, bool isMarkPrint)
    {
        char[] clearChara = clearStr.ToCharArray();
        missNum = 0;
        for (int i = 0; i < clearChara.Length; i++)
        {
            bool clear = false;
            // 回答した文字列の中で正解の文字を含んでいた場合
            for (int j = 0; j < answerStr.Length; j++)
            {
                // 表示文字リストで同じ文字の場合、正解と判定
                if (charList[i] == charList[j])
                {
                    clear = true;
                }
            }

            // 正解だったら(文字を含むか)
            if (clear)
            {
                // isMarkPrintがtrueの場合は〇で囲む
                if (isMarkPrint)
                {
                    int clearNo = (int)clearChara[i] - 12353;
                    GameObject markObj = Instantiate(markClear.gameObject, charList[clearNo].transform);
                    markObj.transform.localPosition = new Vector3(-100, -25, 0);
                    markObj.SetActive(true);
                    markObjList.Add(markObj);
                }
            }
            // 不正解だったら(文字を含んでなかった)
            else
            {
                // 間違い文字数を追加
                missNum++;
            }
        }

        // 間違い文字数が0の場合はクリア
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


