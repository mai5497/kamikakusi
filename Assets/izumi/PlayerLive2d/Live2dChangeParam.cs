//=============================================================================
//
// Live2Dのパラメータ変更
//
// 作成日:2022/10/22
// 作成者:泉優樹
//
// <開発履歴>
// 2022/10/22 作成
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework;

public class Live2dChangeParam : MonoBehaviour
{
    [Header("Live2Dモデル")]
    public CubismModel model;
    [Header("パラメータ番号")]
    public int paramId;
    [Header("値")]
    public float value;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        model.Parameters[paramId].Value = value;
    }
}
