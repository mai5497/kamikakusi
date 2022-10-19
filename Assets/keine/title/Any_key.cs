using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Any_key : MonoBehaviour
{

    public Fade_title_haikei1 titlle;
    //タイトルが終わったか
    bool Titlle;

    private Text text;
    //タイトルロゴが出たか
    public Fade_titlle_logo fade_titlle;
    bool fadeIn_false;

    float timer = 0.0f;
    //点滅
    float tikutaku = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.color = new Color(1, 1, 1, 0);

    }

    // Update is called once per frame
    void Update()
    {
        Titlle = titlle.title_finish;
        fadeIn_false = fade_titlle.fadeIn;


      //  Debug.Log(fadeIn_false);
        if (!fadeIn_false)
        {
           // text.color = new Color(1, 1, 1, 0);





            if (Titlle)
            {
                //  Debug.Log("any");
                text.color = new Color(1, 1, 1, 0 );

            }
            else
            {
                text.color = new Color(1, 1, 1, 0 + tikutaku);

            }
            if (tikutaku <= 0)
            {
                tikutaku = 1;
            }
            if (tikutaku >= 1)
            {
                tikutaku = 0;
            }
            tikutaku += Time.deltaTime;
            timer += Time.deltaTime;
        }
    }
}
