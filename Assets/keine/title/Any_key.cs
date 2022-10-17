using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Any_key : MonoBehaviour
{

    public Fade_title titlle;
    bool Titlle;

    private Image ima;

    float timer = 0.0f;
    float tikutaku = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        ima = GetComponent<Image>();
        ima.color = new Color(1, 1, 1, 1);

    }

    // Update is called once per frame
    void Update()
    {
        Titlle = titlle.title_finish;
        



        if(Titlle)
        {
            Debug.Log("any");
            ima.color = new Color(1, 1, 1, 1- timer);

        }
        else
        {
            ima.color = new Color(1, 1, 1, 1 - tikutaku);

        }
        if(tikutaku<=0)
        {
            tikutaku = 1;
        }
         if(tikutaku>=1)
        {
            tikutaku=0;
        }
        tikutaku += Time.deltaTime;
        timer += Time.deltaTime;
    }
}
