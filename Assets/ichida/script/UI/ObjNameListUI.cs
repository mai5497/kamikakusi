using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjNameListUI : MonoBehaviour
{
    [Header("狐だったらtrue")]
    [SerializeField]
    private bool kituneORnormal;    // 狐か人か

    private string[] nameList;  // オブジェクトの文字列を入れる
    private GameObject[] objList;   // オブジェクト自体を入れる

    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();

        objList = GameObject.FindGameObjectsWithTag("SearchObject"); // タグが付いた全てのオブジェクトを取得する

        nameList = new string[objList.Length];

        for(int i = 0;i < objList.Length; i++) {
            if (kituneORnormal) {
                nameList[i] = objList[i].GetComponent<HintObj>().GetObjName();
            } else {
                nameList[i] = objList[i].GetComponent<HintObj>().GetUraObjName();
            }
        }

        for (int i = 0; i < nameList.Length; i++) {
            text.text = text.text + nameList[i]+ "\n" ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
