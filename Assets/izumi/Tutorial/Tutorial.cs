//=============================================================================
//
// チュートリアル処理
//
// 作成日:2022/10/18
// 作成者:泉優樹
//
// <開発履歴>
// 2022/10/18 作成
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour
{
    // これがtrueの時はInputAssetのActionMapsのTutorial以外(PlayerとUI)を消してもらえると助かります
    [Header("チュートリアル表示フラグ")]
    public bool isTutorial;

    [Header("プレイヤーオブジェクト")]
    public GameObject playerObj;
    [Header("チュートリアル親オブジェクト")]
    public GameObject tutorialParentObj;
    [Header("チュートリアル画像")]
    public SpriteRenderer tutorialImage;
    [Header("チュートリアルリスト画像リスト")]
    public List<Sprite> tutorialImageList;

    [Header("チュートリアル番号")]
    public int tutorialNo;
    [Header("チュートリアル最小番号")]
    public int tutorialNoMin;
    [Header("チュートリアル最大番号")]
    public int tutorialNoMax;

    [Header("決定")]
    private bool isInputDicision;
    private InputAction dicisionAction;

    // Start is called before the first frame update
    void Start()
    {
        var playerInput = this.GetComponent<PlayerInput>();
        var actionMap = playerInput.currentActionMap;
        dicisionAction = actionMap["Dicision"];
        dicisionAction.canceled += OnDicision;

        tutorialParentObj.SetActive(false);
        tutorialNo = tutorialNoMin;
    }

    // Update is called once per frame
    void Update()
    {
        // チュートリアル表示
        if (isTutorial == true)
        {
            tutorialParentObj.SetActive(true);
            tutorialImage.sprite = tutorialImageList[tutorialNo - tutorialNoMin];

            // 決定ボタンを押したら,次に進む
            if (isInputDicision)
            {
                tutorialNo++;
                
                // チュートリアル終了
                if (tutorialNo > tutorialNoMax)
                {
                    tutorialNo = tutorialNoMin;
                    tutorialParentObj.SetActive(false);
                    isTutorial = false;
                }

                isInputDicision = false;
            }
        }
    }

    // チュートリアル表示する当たり判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == playerObj)
        {
            isTutorial = true;
        }
    }

    // 決定ボタン
    private void OnDicision(InputAction.CallbackContext obj)
    {
        isInputDicision = !isInputDicision;
    }
}
