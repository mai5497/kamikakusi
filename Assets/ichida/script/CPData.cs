//=============================================================================
//
// �Q�[����̃f�[�^���Ǘ�����N���X
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

public class CPData : MonoBehaviour
{
    /*
     * �v���C���[�̃f�[�^������ۑ�����N���X�ɂ�����肾��������
     * ���낢��ێ�����N���X�ɂȂ��������
     * 
     * ���󂪕t���Ă���̂̓f�o�b�O�p�ɃQ�[���}�l�[�W���[�Œl���ύX�ł���悤�ɂȂ��Ă��邽��
     * ���̃N���X�̏������q�t���錾�����������Ă�����͕ς��Ȃ��̂Œ���
     */

    public static  bool isLook = false;     // �������Ă���t���O
    public static bool isLens = false;      // �����Ă��邩�����Y�g���Ă邩
    
    public static bool isPose = false;      //�|�[�Y���Ă��邩�t���O

    public static Vector2 playerPos = Vector2.zero; // �v���C���[�̍��W    

    public static bool isRightAnswer = false;   // �`���Ă�Ƃ��낪�����Ă�̂ł��ꂠ��true

    public static bool isKokkurisan = false;      // �������肳���ʂ������Ă邩
    public static bool kokkurisanButton = false;    // �������肳��̉�ʂ̂ڂ��񂪂�����Ă邩�ǂ���

    public static bool isObjNameUI = false;      // �q���g�ƃI�u�W�F�N�g�̖��O��\������ 

    public static int paperCnt = 5;         // �������邱�Ƃ��ł���񐔁i���j
    public static int lookCnt = 5;          // �����ł���񐔁i���j

    public static string kituneClearStr;    // �ς̖ڂŌ������̐����i���j
    public static string normalClearStr;    // �l�̖ڂŌ������̐����i���j

    public static string kituneHint;        // �ς̖ڂ̃q���g(��)
    public static string normalHint;        // �l�̖ڂ̃q���g(��)
}
