//=============================================================================
//
// Live2Dのパラメータをプログラムで変更できるか確認
//
// 作成日:2022/10/10
// 作成者:泉優樹
//
// <開発履歴>
// 2022/10/10 作成
// 2022/10/13 テストプロジェクト→共有のプロジェクトに移動
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework;

public class TestMotion : MonoBehaviour
{
    // モデル
    public CubismModel model;

    //パラメータ
    [System.Serializable]
    private struct ParamStruct
    {
        // パラメータ名
        public string parameterID;
        // 書き換えタイプ
        public enum Type
        {
            Add,
            Mul,
            Override,
            None
        }
        public Type type;
        // パラメータ変更量
        public float value;
    }
    [SerializeField]
    private List<ParamStruct> paramStructList;

    [SerializeField]
    private bool _paramChangeFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        //model = this.FindCubismModel();
    }

    // Update is called once per frame
    void Update()
    {
        if (_paramChangeFlg == true)
        {
            for (int i = 0; i < paramStructList.Count; i++)
            {
                switch (paramStructList[i].type)
                {
                    // 加算
                    case ParamStruct.Type.Add:
                        model.Parameters.FindById(paramStructList[i].parameterID).BlendToValue(CubismParameterBlendMode.Additive, paramStructList[i].value);
                        break;
                    // 乗算
                    case ParamStruct.Type.Mul:
                        model.Parameters.FindById(paramStructList[i].parameterID).BlendToValue(CubismParameterBlendMode.Multiply, paramStructList[i].value);
                        break;
                    // 上書き
                    case ParamStruct.Type.Override:
                        model.Parameters.FindById(paramStructList[i].parameterID).BlendToValue(CubismParameterBlendMode.Override, paramStructList[i].value);
                        break;
                    // 何もしない
                    case ParamStruct.Type.None:
                        break;
                }
            }
            _paramChangeFlg = false;
        }
    }
}