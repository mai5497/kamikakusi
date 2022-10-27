using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperCountUI : MonoBehaviour
{
    private Text paperCntText;

    private Image paperUI;

    // Start is called before the first frame update
    void Start() {
        paperCntText = GetComponent<Text>();
        paperUI = transform.Find("Paper").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update() {
        paperCntText.text = CPData.paperCnt.ToString();
        if (CPData.paperCnt <= 0) {
            paperUI.color = new Color(0.7f, 0.7f, 0.7f, 1.0f);
        }

    }
}
