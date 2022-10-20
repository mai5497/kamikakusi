//=============================================================================
//
// �V�[���}�l�[�W���[�f�[�^
//
// �쐬��:2022/10/20
// �쐬��:��D��
//
// <�J������>
// 2022/10/20 �쐬
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerData
{
    [Header("�^�C�g���V�[����")]
    public static string titleScene = "title";
    [Header("�X�e�[�W�Z���N�g�V�[����")]
    public static string stageSelectScene = "StageSelect";
    [Header("���C���V�[�������X�g")]  // string[���[���h�ԍ�,�X�e�[�W�ԍ�]
    public static string[,] mainSceneStrArray =
    {
        { "Tutorial","Tutorial","Tutorial" },
        { "Stage1_01","Stage1_02","Stage1_03" },
        { "Stage2_01","Stage2_02","Stage2_03" },
        { "Stage3_01","Stage3_02","Stage3_03" },
    };
    [Header("�t�F�[�h�A�E�g���x")]
    public static float speedFadeOut = 0.5f;
    [Header("�t�F�[�h�C�����x")]
    public static float speedFadeIn = 1.0f;

    // ���݂̃��[���h�ԍ�
    public static int nowWorldNo = 0;
    // ���݂̃X�e�[�W�ԍ�
    public static int nowStageNo = 0;
}
