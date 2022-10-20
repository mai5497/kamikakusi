using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstHintUIChar : MonoBehaviour
{
    [Header("�ς��l��(true����)")]
    [SerializeField]
    private bool kituneORnormal = true;

    private Text hintCharText;
    private string firstHintKitune;
    private string firstHintNormal;


    // Start is called before the first frame update
    void Start() {
        hintCharText = GetComponent<Text>();

        firstHintKitune = CPData.kituneHint;
        firstHintNormal = CPData.normalHint;

        //----- �����̕������烉���_���ňꕶ���擾���� -----
        int random;
        if (kituneORnormal) {
            if (firstHintKitune == "") {
                random = UnityEngine.Random.Range(0, CPData.kituneClearStr.Length);
                firstHintKitune = CPData.kituneClearStr.Substring(random, 1);
                CPData.kituneHint = firstHintKitune;
            }
        } else {
            if (firstHintNormal == "") {
                random = UnityEngine.Random.Range(0, CPData.normalClearStr.Length);
                firstHintNormal = CPData.normalClearStr.Substring(random, 1);
                CPData.normalHint = firstHintNormal;
            }
        }

        //----- �����̕\�� -----
        if (kituneORnormal) {
            hintCharText.text = firstHintKitune;
        } else {
            hintCharText.text = firstHintNormal;
        }
    }

    // Update is called once per frame
    void Update() {
    }
}
