//=============================================================================
//
// �Q�[����UI�̊Ǘ�
//
// �쐬��:2022/10/17
// �쐬��:�ɒn�c�^��
//
// <�J������>
// 2022/10/17 �쐬
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameUIManager : MonoBehaviour
{
    private GameObject canvasObj;       // UI�\���̂��߂̃L�����o�X������I�u�W�F�N�g�̊i�[
    private Canvas canvas;              // UI�\���̂��߂̃L�����o�X

    [SerializeField]
    private GameObject gameUI;          // UI�̂���p�l�����C���X�y�N�^�[�Ŋi�[
    private GameObject gameUIEntity;    // Instatiate�Ŏ��̉�������p�̕ϐ�

    [SerializeField]
    private GameObject hintUI;         // �q���g�ƃI�u�W�F�N�g�̖��O��\������UI
    private GameObject hintUIEntity;   // �q���g�ƃI�u�W�F�N�g�̖��O��\������UI�̎���

    private CP_move_input UIActionAssets;        // InputAction��UI������

    private GameObject tutorial;
    private Tutorial _Tutorial;


    void Awake() {
        UIActionAssets = new CP_move_input();            // InputAction�C���X�^���X�𐶐�
    }

    // Start is called before the first frame update
    void Start() {
        tutorial = GameObject.Find("FoxTutorial");
        if (tutorial) {
            _Tutorial = tutorial.GetComponent<Tutorial>();
        }

        //----- �L�����o�X��������Ȃ�������L�����o�X���쐬���� -----
        canvasObj = GameObject.Find("Canvas");
        if (canvasObj) {
            canvas = canvasObj.GetComponent<Canvas>();
        } else {
            canvasObj = new GameObject();
            canvasObj.name = "Canvas";
            canvasObj.AddComponent<Canvas>();

            canvas = canvasObj.GetComponent<Canvas>();
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
        }
        // �L�����o�X�̐ݒ�
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
        canvas.sortingLayerName = "TopLayer";
        canvas.sortingOrder = 3;


        //----- �f�o�b�O�p -----
        if (!gameUI) {
#if UNITY_EDITOR
            Debug.LogError("<color=red>gameUI�̃p�l������Y��Ă�I</color>");
#endif
        }else {
            gameUIEntity = Instantiate(gameUI);
            gameUIEntity.transform.SetParent(canvas.transform, false);
        }

        if (!hintUI) {
#if UNITY_EDITOR
            Debug.LogError("<color=red>hintUI�̃p�l������Y��Ă�I</color>");
#endif
        } else {
            hintUIEntity = Instantiate(hintUI);
            hintUIEntity.transform.SetParent(canvas.transform, false);
            hintUIEntity.SetActive(false);   // �n�߂͔�\��
        }
    }

    private void OnEnable() {
        //---Action�C�x���g��o�^
        UIActionAssets.UI.Switching.started += OnSwitchUI;

        //---InputAction�̗L����
        UIActionAssets.UI.Enable();
    }

    private void OnDisable() {
        //---InputAction�̖�����
        UIActionAssets.UI.Disable();
    }



    // Update is called once per frame
    void Update()
    {
        if (CPData.isObjNameUI) {
            hintUIEntity.SetActive(true);
            gameUIEntity.SetActive(false);
        } else {
            hintUIEntity.SetActive(false);
            gameUIEntity.SetActive(true);
        }
    }

    private void OnSwitchUI(InputAction.CallbackContext obj) {
        if (tutorial) {
          
            if (_Tutorial.isTutorial) {
                return;
            }
        }
        if (CPData.isPose)
        {
            return;
        }
        CPData.isObjNameUI = !CPData.isObjNameUI;
    }
}
