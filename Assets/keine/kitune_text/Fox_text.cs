using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Fox_text : MonoBehaviour
{
    private Vector2 Pos;
    public Transform transform_oya;
    public Text text;
    private RectTransform rec;
    public CP_move01 move;
    public List<string> TextList = new List<string>();
    [Header("会話の行数いれてね＾＿＾")]
    public int countMax = 0;

    [Header("チュートリアルリスト画像リスト")]
    public List<Sprite> ImageList;

    [Header("チュートリアル画像")]
    public SpriteRenderer GazouImage;


    private int count = 0;

    public Fade_out003 FadeOut;
    private bool isFadeOut_Finish;
    public bool isTextFin = false;

    private InputAction _fadeAction;
    // Start is called before the first frame update
    void Start()
    {
        Pos = new Vector2(this.transform.position.x, this.transform.position.y);
        rec = GetComponent<RectTransform>();

        var pInput = GetComponent<PlayerInput>();
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
        _fadeAction = actionMap["Fade"];


        text.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);


        //  count = TextNo.Count;

    }

    // Update is called once per frame
    void Update()
    {
        isFadeOut_Finish = FadeOut.fadeOut_finish;


        //テキストの場所
        Text_Pos();
        //テキストの中身
        Text();


    }









    public void Text_Pos()
    {
        Pos.x = transform_oya.parent.position.x;
        Pos.y = transform_oya.parent.position.y + rec.position.y;
    }
    public void Text()
    {
     
        bool Fade = _fadeAction.WasPerformedThisFrame();
        if (Fade)
        {
            // text.text = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            count += 1;
        }

        if (isFadeOut_Finish)
        {
            text.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        }

        if (count < countMax)
        {
            text.text = TextList[count];
           GazouImage.sprite = ImageList[count];
        }
        else
        {
            text.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            CPData.isPose = false;
            //        //  move.OnEnable();
            //        //  isTextFin = true;
        }
    }
}

//public bool GetTextFinish()
//{
//    return isTextFin;
//}}
