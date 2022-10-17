using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstHintUICnt : MonoBehaviour
{
    [Header("åœÇ©êlÇ©(trueÇ™åœ)")]
    [SerializeField]
    private bool kituneORnormal = true; 

    private Text hintCntText;

    private int hintCnt;

    // Start is called before the first frame update
    void Start() { 
        hintCntText = GetComponent<Text>();

        if (kituneORnormal) {
            hintCnt = CPData.kituneClearStr.Length - 1;
        } else {
            hintCnt = CPData.normalClearStr.Length - 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        hintCntText.text = hintCnt.ToString();
    }
}
