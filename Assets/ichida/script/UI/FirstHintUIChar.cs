using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstHintUIChar : MonoBehaviour
{
    [Header("ŒÏ‚©l‚©(true‚ªŒÏ)")]
    [SerializeField]
    private bool kituneORnormal = true;

    private Text hintCharText;
    private string firstHintKitune;
    private string firstHintNormal;

    // Start is called before the first frame update
    void Start() {
        hintCharText = GetComponent<Text>();

        //----- “š‚¦‚Ì•¶š‚©‚çƒ‰ƒ“ƒ_ƒ€‚Åˆê•¶šæ“¾‚·‚é -----
        int random;
        if (kituneORnormal) {
            random = UnityEngine.Random.Range(0, CPData.kituneClearStr.Length);
            firstHintKitune = CPData.kituneClearStr.Substring(random, 1);
        } else {
            random = UnityEngine.Random.Range(0, CPData.normalClearStr.Length);
            firstHintNormal = CPData.normalClearStr.Substring(random, 1);
        }

        //----- •¶š‚Ì•\¦ -----
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
