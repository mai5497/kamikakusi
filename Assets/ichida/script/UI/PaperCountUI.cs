using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperCountUI : MonoBehaviour
{
    private Text paperCntText;

    // Start is called before the first frame update
    void Start() {
        paperCntText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        paperCntText.text = CPData.paperCnt.ToString();
    }
}
