//=============================================================================
//
// Live2D�̃p�����[�^���v���O�����ŕύX�ł��邩�m�F
//
// �쐬��:2022/10/10
// �쐬��:��D��
//
// <�J������>
// 2022/10/10 �쐬
// 2022/10/13 �e�X�g�v���W�F�N�g�����L�̃v���W�F�N�g�Ɉړ�
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework;

public class TestMotion : MonoBehaviour
{
    // ���f��
    public CubismModel model;

    //�p�����[�^
    [System.Serializable]
    private struct ParamStruct
    {
        // �p�����[�^��
        public string parameterID;
        // ���������^�C�v
        public enum Type
        {
            Add,
            Mul,
            Override,
            None
        }
        public Type type;
        // �p�����[�^�ύX��
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
                    // ���Z
                    case ParamStruct.Type.Add:
                        model.Parameters.FindById(paramStructList[i].parameterID).BlendToValue(CubismParameterBlendMode.Additive, paramStructList[i].value);
                        break;
                    // ��Z
                    case ParamStruct.Type.Mul:
                        model.Parameters.FindById(paramStructList[i].parameterID).BlendToValue(CubismParameterBlendMode.Multiply, paramStructList[i].value);
                        break;
                    // �㏑��
                    case ParamStruct.Type.Override:
                        model.Parameters.FindById(paramStructList[i].parameterID).BlendToValue(CubismParameterBlendMode.Override, paramStructList[i].value);
                        break;
                    // �������Ȃ�
                    case ParamStruct.Type.None:
                        break;
                }
            }
            _paramChangeFlg = false;
        }
    }
}