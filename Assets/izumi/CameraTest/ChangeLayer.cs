//=============================================================================
//
// 子オブジェクト全てのレイヤーを自分のレイヤーに変更
//
// 作成日:2022/10/21
// 作成者:泉優樹
//
// <開発履歴>
// 2022/10/21 作成
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform childTransform in this.transform)
        {
            childTransform.gameObject.layer = this.gameObject.layer;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
