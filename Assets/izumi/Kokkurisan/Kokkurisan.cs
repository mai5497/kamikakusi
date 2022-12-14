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
    [Header("正解した場合に表示する丸(人の目)")]
    public Image markClearNormal;
    [Header("正解した場合に表示する丸(狐の目)")]
    public Image markClearFox;
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

    [Header("こっくりさん片方クリア")]
    public bool isSideClear;

    [Header("両方0(クリア)エフェクト")]
    public List<ParticleSystem> ef0 = new List<ParticleSystem>();

    [Header("片方0エフェクト")]
    public List<ParticleSystem> ef0_1 = new List<ParticleSystem>();

    [Header("狐の窓")]
    private Image frame;
    [Header("狐の窓の通常時")]
    public Sprite frameNormal;
    [Header("狐の窓のものを見つけた時")]
    public Sprite frameFind;

    [Header("狐の窓で物を見つけたか")]
    public static bool isFind;

    [Header("〇が付いた文字_人の目")]
    public string normalMaruStr;
    [Header("〇が付いた文字_狐の目")]
    public string foxMaruStr;

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

    private Canvas canvas;  // キャンバスにメインカメラを設定するために取得

    // Start is called before the first frame update
    void Start()
    {
        markClearNormal.gameObject.SetActive(false);
        markClearFox.gameObject.SetActive(false);
        canvas = GetComponent<Canvas>();

        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
        canvas.sortingLayerName = "TopLayer";
        canvas.sortingOrder = 2;    // 窓より上に表示

        frame = GameObject.FindGameObjectWithTag("Frame").GetComponent<Image>();
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
            isFind = true;
        }
        else
        {
            frame.sprite = frameNormal;
            isFind = false;
        }

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

                    // 〇を囲む時(こっくりさんの表示した瞬間,目の切り替えた瞬間)にクリアだったら、エフェクトを再生
                    if (isClear)
                    {
                        foreach(ParticleSystem ef in ef0)
                        {
                            ef.Play();
                        }
                        SoundManager2.Play(SoundData.eSE.SE_HIT, SoundData.GameAudioList);
                    }
                    if (isSideClear)
                    {
                        foreach (ParticleSystem ef in ef0_1)
                        {
                            ef.Play();
                        }
                        SoundManager2.Play(SoundData.eSE.SE_HIT, SoundData.GameAudioList);
                    }
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
            normalNoImage.gameObject.SetActive(false);
            foxYesImage.gameObject.SetActive(false);
            normalYesImage.gameObject.SetActive(true);
            foxNoImage.gameObject.SetActive(true);
        }
        // 狐の目
        else if (isFox)
        {
            normalYesImage.gameObject.SetActive(false);
            foxNoImage.gameObject.SetActive(false);
            foxYesImage.gameObject.SetActive(true);
            normalNoImage.gameObject.SetActive(true);
        }
        // どちらでもない
        else
        {
            normalYesImage.gameObject.SetActive(false);
            foxYesImage.gameObject.SetActive(false);
            normalNoImage.gameObject.SetActive(true);
            foxNoImage.gameObject.SetActive(true);
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

        // エフェクトクリア
        foreach (ParticleSystem ef in ef0)
        {
            ef.Clear();
        }
        foreach (ParticleSystem ef in ef0_1)
        {
            ef.Clear();
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
            if (answerStr.Contains(clearChara[i]))
            {
                clear = true;
            }

            // 正解だったら(文字を含むか)
            if (clear)
            {
                // isMarkPrintがtrueの場合は〇で囲む
                if (isMarkPrint)
                {
                    int clearNo = (int)clearChara[i] - 12353;
                    GameObject markClearObj = markClearNormal.gameObject;
                    if (isNormal)
                    {
                        markClearObj = markClearNormal.gameObject;
                    }
                    else if (isFox)
                    {
                        markClearObj = markClearFox.gameObject;
                    }
                    GameObject markObj = Instantiate(markClearObj, charList[clearNo].transform);
                    markObj.transform.localPosition = new Vector3(-100, -25, 0);
                    markObj.SetActive(true);
                    markObjList.Add(markObj);
                    // 〇で囲まれた文字をmaruStrに格納
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


