//=============================================================================
//
// Live2D�̃p�����[�^�ύX
//
// �쐬��:2022/10/22
// �쐬��:��D��
//
// <�J������>
// 2022/10/22 �쐬
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework;

public class Live2dChangeParam : MonoBehaviour
{
    [Header("Live2D���f��")]
    public CubismModel model;
    [Header("�p�����[�^�ԍ�")]
    public int paramId;
    [Header("�l")]
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
