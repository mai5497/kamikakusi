//=============================================================================
//
// �q���g��UI�i�����̕��j�ɂ��邽�߂̃X�N���v�g
//
// �쐬��:2022/10/12
// �쐬��:�ɒn�c�^��
//
// <�J������>
// 2022/10/12 �쐬
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintUI : MonoBehaviour
{
    [SerializeField]
    private int hintNum = 0;    // �q���g�̃I�u�W�F�N�g�ɑΉ�����ԍ�

    public  int HintNum {
        // ���������͂��Ȃ��̂�getter�̂ݍ쐬
        get {
            return hintNum;
        }
    }
}
