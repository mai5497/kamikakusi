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

public class GameUIManager : MonoBehaviour
{
    private GameObject canvasObj;   // UI�\���̂��߂̃L�����o�X������I�u�W�F�N�g�̊i�[
    private Canvas canvas;  // UI�\���̂��߂̃L�����o�X

    [SerializeField]
    private GameObject gameUI;          // UI�̂���p�l�����C���X�y�N�^�[�Ŋi�[
    private GameObject gameUIEntity;    // Instatiate�Ŏ��̉�������p�̕ϐ�

    // Start is called before the first frame update
    void Start() {
        //----- �L�����o�X��������Ȃ�������L�����o�X���쐬���� -----
        canvasObj = GameObject.Find("Canvas");
        if (canvasObj) {
            canvas = canvasObj.GetComponent<Canvas>();
        } else {
            canvasObj = new GameObject();
            canvasObj.name = "Canvas";
            canvasObj.AddComponent<Canvas>();

            canvas = canvasObj.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
        }

        //----- �f�o�b�O�p -----
        if (!gameUI) {
#if UNITY_EDITOR
            Debug.LogError("<color=red>UI�̃p�l������Y��Ă�I</color>");
#endif
        }else {
            gameUIEntity = Instantiate(gameUI);
            gameUIEntity.transform.SetParent(canvas.transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
