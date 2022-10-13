//=============================================================================
//
// レンズの表示,非表示処理
//
// 作成日:2022/10/12
// 作成者:泉優樹
//
// <開発履歴>
// 2022/10/12 作成
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableLens : MonoBehaviour
{
    [Header("レンズの可視化時に表示する画像リスト")]
    public List<Image> imageList;
    // Start is called before the first frame update
    void Start()
    {
        // レンズを非表示
        EnableImage(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // レンズの表示非表示
    public void EnableImage(bool isEnable)
    {
        for(int i = 0; i < imageList.Count; i++)
        {
            imageList[i].enabled = isEnable;
        }
    }
}
