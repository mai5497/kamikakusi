//=============================================================================
//
// �ς̑��̐���
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

public class LensManager : MonoBehaviour
{
    [Header("�ڂ���")]
    [SerializeField]
    private BlurIn blurIn;
    [Header("�����Y�I�u�W�F�N�g")]
    [SerializeField]
    private GameObject lensObj;
    [Header("�Y�[��")]
    [SerializeField]
    private ZoomLens zoomLens;
    [Header("�����Y���x�{��")]
    [SerializeField]
    private float lensSpeed = 0.05f;


    private CP_move01 _Player;

    // �����Y��
    private EnableLens enableLens;

    private bool oldIsLens;         // islens��true�ɂȂ����u�Ԃ݂̂��鏈���p
    private Vector2 oldPlayerPos;   // �ړ����Ă��Ȃ���ΐ�قǂ܂ŕ\�����Ă����ꏊ�ɕ\������ׂ̕ϐ�

    private bool isLensInit;    // �����Y�̈ʒu�����������邩

    private RectTransform lensRT;

    private RectTransform lensCanvas;

    // Start is called before the first frame update
    void Start()
    {
        _Player = GetComponent<CP_move01>();
        // �����Y���̎擾
        enableLens = lensObj.GetComponent<EnableLens>();
        lensRT = lensObj.GetComponent<RectTransform>();

        lensCanvas = GameObject.Find("CanvasLens").GetComponent<RectTransform>();

        oldIsLens = CPData.isLens;
        oldPlayerPos = CPData.playerPos;
    }

    // Update is called once per frame
    void Update() {
        if(oldPlayerPos != CPData.playerPos) {
            isLensInit = true;

            oldPlayerPos = CPData.playerPos;
        }

        if (oldIsLens != CPData.isLens) {
            if (blurIn.isBlur == false) {
                // �ڂ���,�����Y�̗L��
                blurIn.isBlur = true;
                enableLens.EnableImage(true);
            } else {
                // �����Y�̖���
                blurIn.isBlur = false;
                enableLens.EnableImage(false);
            }

            oldIsLens = CPData.isLens;
        }

        // �����Y�ړ�(�ʏ펞�̂�)
        if (!CPData.isKokkurisan && !CPData.isObjNameUI && CPData.isLens && blurIn.blurMode == BlurIn.BlurMode.Normal) {
            if (isLensInit) {
                lensRT.position = CPData.playerPos;
                isLensInit = false;
            }
            Vector2 moveVal;
            Vector3 newLensPos = lensObj.transform.position;
            moveVal.x = _Player.GetMoveValue().x * lensSpeed;
            moveVal.y = _Player.GetMoveValue().y * lensSpeed;

            newLensPos.x += moveVal.x;
            newLensPos.y += moveVal.y;

            lensObj.transform.position = new Vector2(
                         //�G���A�w�肵�Ĉړ�����
                         Mathf.Clamp(newLensPos.x, -8.5f, 8.5f),
                         Mathf.Clamp(newLensPos.y, -4.5f, 4.5f)
                         ) ; 
        }
        // �����Y�̒���(�ڂ������[�h�ύX)
        if (CPData.isLook && CPData.isLens) {
            if (blurIn.blurMode == BlurIn.BlurMode.Normal) {
                blurIn.blurMode = BlurIn.BlurMode.PressInit;
                // �Y�[������
                zoomLens.isZoom = true;
            }
        } else {
            blurIn.blurMode = BlurIn.BlurMode.Normal;
            // �Y�[����������
            zoomLens.isZoom = false;
        }
    }
}
