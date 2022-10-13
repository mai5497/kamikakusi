//=============================================================================
//
// �����Y�̕\��,��\������
//
// �쐬��:2022/10/12
// �쐬��:��D��
//
// <�J������>
// 2022/10/12 �쐬
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableLens : MonoBehaviour
{
    [Header("�����Y�̉������ɕ\������摜���X�g")]
    public List<Image> imageList;
    // Start is called before the first frame update
    void Start()
    {
        // �����Y���\��
        EnableImage(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // �����Y�̕\����\��
    public void EnableImage(bool isEnable)
    {
        for(int i = 0; i < imageList.Count; i++)
        {
            imageList[i].enabled = isEnable;
        }
    }
}
