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

    private bool oldIsLens;
    private Vector2 oldPlayerPos;

    private bool isLensInit;    // �����Y�̈ʒu�����������邩


    // Start is called before the first frame update
    void Start()
    {
        _Player = GetComponent<CP_move01>();
        // �����Y���̎擾
        enableLens = lensObj.GetComponent<EnableLens>();


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

        //if (!CPData.isHint) {
        //    blurIn.isBlur = false;
        //} else {
        //    blurIn.isBlur = true;
        //}

        // �����Y�ړ�(�ʏ펞�̂�)
        if (!CPData.isHint && CPData.isLens && blurIn.blurMode == BlurIn.BlurMode.Normal) {
            //if (keyboard.aKey.isPressed) {
            //    lensObj.transform.position = new Vector3(lensObj.transform.position.x - moveSpeed, lensObj.transform.position.y, lensObj.transform.position.z);
            //}
            //if (keyboard.dKey.isPressed) {
            //    lensObj.transform.position = new Vector3(lensObj.transform.position.x + moveSpeed, lensObj.transform.position.y, lensObj.transform.position.z);
            //}
            //if (keyboard.sKey.isPressed) {
            //    lensObj.transform.position = new Vector3(lensObj.transform.position.x, lensObj.transform.position.y - moveSpeed, lensObj.transform.position.z);
            //}
            //if (keyboard.wKey.isPressed) {
            //    lensObj.transform.position = new Vector3(lensObj.transform.position.x, lensObj.transform.position.y + moveSpeed, lensObj.transform.position.z);
            //}
            if (isLensInit) {
                lensObj.transform.position = CPData.playerPos;
                isLensInit = false;
            }
            Vector2 moveVal;
            moveVal.x = _Player.GetMoveValue().x * lensSpeed;
            moveVal.y = _Player.GetMoveValue().y * lensSpeed;
            lensObj.transform.position = new Vector3(lensObj.transform.position.x + moveVal.x, lensObj.transform.position.y + moveVal.y, lensObj.transform.position.z);
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
