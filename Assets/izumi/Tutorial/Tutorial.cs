//=============================================================================
//
// �`���[�g���A������
//
// �쐬��:2022/10/18
// �쐬��:��D��
//
// <�J������>
// 2022/10/18 �쐬
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour
{
    // ���ꂪtrue�̎���InputAsset��ActionMaps��Tutorial�ȊO(Player��UI)�������Ă��炦��Ə�����܂�
    [Header("�`���[�g���A���\���t���O")]
    public bool isTutorial;

    [Header("�v���C���[�I�u�W�F�N�g")]
    public GameObject playerObj;
    [Header("�`���[�g���A���e�I�u�W�F�N�g")]
    public GameObject tutorialParentObj;
    [Header("�`���[�g���A���摜")]
    public SpriteRenderer tutorialImage;
    [Header("�`���[�g���A�����X�g�摜���X�g")]
    public List<Sprite> tutorialImageList;

    [Header("�`���[�g���A���ԍ�")]
    public int tutorialNo;
    [Header("�`���[�g���A���ŏ��ԍ�")]
    public int tutorialNoMin;
    [Header("�`���[�g���A���ő�ԍ�")]
    public int tutorialNoMax;

    [Header("����")]
    private bool isInputDicision;
    private InputAction dicisionAction;

    // Start is called before the first frame update
    void Start()
    {
        var playerInput = this.GetComponent<PlayerInput>();
        var actionMap = playerInput.currentActionMap;
        dicisionAction = actionMap["Dicision"];
        dicisionAction.canceled += OnDicision;

        tutorialParentObj.SetActive(false);
        tutorialNo = tutorialNoMin;
    }

    // Update is called once per frame
    void Update()
    {
        // �`���[�g���A���\��
        if (isTutorial == true)
        {
            tutorialParentObj.SetActive(true);
            tutorialImage.sprite = tutorialImageList[tutorialNo - tutorialNoMin];

            // ����{�^������������,���ɐi��
            if (isInputDicision)
            {
                tutorialNo++;
                
                // �`���[�g���A���I��
                if (tutorialNo > tutorialNoMax)
                {
                    tutorialNo = tutorialNoMin;
                    tutorialParentObj.SetActive(false);
                    isTutorial = false;
                }

                isInputDicision = false;
            }
        }
    }

    // �`���[�g���A���\�����铖���蔻��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == playerObj)
        {
            isTutorial = true;
        }
    }

    // ����{�^��
    private void OnDicision(InputAction.CallbackContext obj)
    {
        isInputDicision = !isInputDicision;
    }
}
