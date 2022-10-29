//=============================================================================
//
// �|�X�g�v���Z�X�����p
//
// �쐬��:2022/10/29
// �쐬��:��D��
//
// <�J������>
// 2022/10/29 �쐬
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessGenerator : MonoBehaviour
{
    [Header("���[���h2�̃|�X�g�v���Z�X")]
    public GameObject postprocessStage2;
    [Header("���[���h3�̃|�X�g�v���Z�X")]
    public GameObject postprocessStage3;

    private static bool isPostprocessStage2 = false;
    private static bool isPostprocessStage3 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �t�F�[�h�C�����ɁA�t���O�����Ă΃|�X�g�v���Z�X����
        if (Fade_out003.GetFading())
        {
            if (isPostprocessStage2)
            {
                Instantiate(postprocessStage2);
                isPostprocessStage2 = false;
            }
            if (isPostprocessStage3)
            {
                Instantiate(postprocessStage3);
                isPostprocessStage3 = false;
            }
        }
    }

    // �|�X�g�v���Z�X�̃Z�b�g
    public static void SetPostProcess(int worldNo)
    {
        switch (worldNo) {
            case 2:
                isPostprocessStage2 = true;
                break;
            case 3:
                isPostprocessStage3 = true;
                break;
        }
    }
}
