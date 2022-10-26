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
    private CP_move01 move;
    public List<string> TextList = new List<string>();
    [Header("会話の行数いれてね＾＿＾")]
    public int countMax;

    [Header("チュートリアルリスト画像リスト")]
    public List<Sprite> ImageList;

    [Header("チュートリアル画像")]
    public SpriteRenderer GazouImage;


    public int count;


    private Fade_out003 Fade;
    //  public Fade_out003 FadeOut;
    private bool isFadeOut_Finish;
    public bool isTextFin ;

    private InputAction _fadeAction;
    // Start is called before the first frame update
    void Start()
    {
        Pos = new Vector2(this.transform.position.x, this.transform.position.y);
        rec = GetComponent<RectTransform>();

        var pInput = GetComponent<PlayerInput>();
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
        _fadeAction = actionMap["Dicision"];


        text.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        count = 0;
        //  count = TextNo.Count;
        isTextFin = false;

        Fade = GameObject.Find("fadeIn").GetComponent<Fade_out003>();
        move = GameObject.Find("CP_001").GetComponent<CP_move01>();
    }

    // Update is called once per frame
    void Update()
    {
        isFadeOut_Finish = Fade.fadeOut_finish;


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
        //  Debug.Log(count + "asd" + countMax);

        Debug.Log(isFadeOut_Finish + "11asd" + countMax);
        if (isFadeOut_Finish)
        {
            text.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);

            bool Dicision = _fadeAction.WasPerformedThisFrame();
            Keyboard keyboard = Keyboard.current;
            if (keyboard.enterKey.wasReleasedThisFrame)
            {
                // text.text = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                count += 1;
            }


            //  Debug.Log(count+"asd"+ countMax);
            if (count < countMax)
            {
                text.text = TextList[count];
                if(count<ImageList.Count)
                {
                    if (ImageList[count] == null)
                    {
                        GazouImage.enabled = false;
                    }
                    else
                    {
                        GazouImage.enabled = true;
                        GazouImage.sprite = ImageList[count];
                    }
                }
                else
                {
                    GazouImage.enabled = false;
                }

            }
            else
            {
                text.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                CPData.isPose = false;
                isTextFin = true;
                // count = 0;
                //        //  move.OnEnable();
                //        //  isTextFin = true;
            }
        }
    }


    public bool GetTextFinish()
    {
        return isTextFin;
    }
}