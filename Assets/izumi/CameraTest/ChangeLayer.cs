//=============================================================================
//
// �q�I�u�W�F�N�g�S�Ẵ��C���[�������̃��C���[�ɕύX
//
// �쐬��:2022/10/21
// �쐬��:��D��
//
// <�J������>
// 2022/10/21 �쐬
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
