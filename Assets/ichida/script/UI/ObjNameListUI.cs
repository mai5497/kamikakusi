//=============================================================================
//
// オブジェクトの名前のUI
//
// 作成日:2022/10/17
// 作成者:伊地田真衣
//
// <開発履歴>
// 2022/10/17 作成
// 2022/10/18 狐人重複無しで表示
//            アイコン表示できる
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjNameListUI : MonoBehaviour
{
    [SerializeField]
    private GameObject iconObj;     // アイコン用のオブジェクト元

    private List<string> nameList;  // オブジェクトの文字列を入れる
    private List<Sprite> spriteList;// オブジェクトのアイコンとしてスプライトを取得してくる
    private GameObject[] objList;   // オブジェクト自体を入れる

    private GameObject[] textObj = new GameObject[2];   // 左右に分かれているので２こ
    private Text rightText;
    private Text leftText;

    private Image[] iconImage;   // アイコン表示用スプライト
    private GameObject[] iconObjEntity; // アイコン表示オブジェクトの実体

    // Start is called before the first frame update
    void Start()
    {
        //----- 文字を表示するテキストボックスを左右に分けたからそれぞれ取得 -----
        textObj[0] = transform.GetChild(0).gameObject;  // 左
        textObj[1] = transform.GetChild(1).gameObject;  // 右

        leftText = textObj[0].GetComponent<Text>();
        rightText = textObj[1].GetComponent<Text>();


        objList = GameObject.FindGameObjectsWithTag("SearchObject"); // タグが付いた全てのオブジェクトを取得する

        nameList = new List<string>();
        spriteList = new List<Sprite>();

        //----- 狐と人側のオブジェクトの名前を重複無しで格納する -----
        for (int i = 0; i < objList.Length; i++) {
            string work;   // 一時的に名前を格納するための変数

            //----- 人の側のオブジェクトの格納 -----
            work = objList[i].GetComponent<HintObj>().GetObjName(); // 人側の名前の格納
            if (!nameList.Contains(work)) {  // リストに要素が無ければ追加
                nameList.Add(work);
                spriteList.Add(objList[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite); // アイコン用にspriteを抜き取る
            }
            //----- 狐の側のオブジェクトの格納 -----
            work = objList[i].GetComponent<HintObj>().GetUraObjName();  // 狐側の名前の格納
            if (!nameList.Contains(work)) {  // リストに要素が無ければ追加
                nameList.Add(work);
                spriteList.Add(objList[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite);// アイコン用にspriteを抜き取る
            }
        }

        //----- アイコン表示 -----
        iconImage = new Image[nameList.Count];
        iconObjEntity = new GameObject[nameList.Count];

        for (int i = 0; i < nameList.Count; i++) {
            iconObjEntity[i] = Instantiate(iconObj);    // 実体化
            iconObjEntity[i].transform.SetParent(this.transform, false);// このオブジェクトの子オブジェクトにする
            iconImage[i] = iconObjEntity[i].GetComponent<Image>();
            iconImage[i].sprite = spriteList[i];    // あらかじめ抜き取っておいたspriteを取得する
            iconImage[i].transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);  // ちょこっと小さくして表示

            if (i > 4) {    // 右側
                rightText.text = rightText.text + nameList[i] + "\n";
                iconObjEntity[i].transform.localPosition = new Vector3(rightText.transform.localPosition.x - 250, rightText.transform.localPosition.y - (i - 7) * 100, -5);
            } else {    // 左側
                leftText.text = leftText.text + nameList[i] + "\n";
                iconObjEntity[i].transform.localPosition = new Vector3(leftText.transform.localPosition.x - 250, leftText.transform.localPosition.y - (i - 2) * 100, -5);
            }
        }
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
