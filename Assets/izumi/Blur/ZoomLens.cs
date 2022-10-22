//=============================================================================
//
// �������̃Y�[������
//
// �쐬��:2022/10/13
// �쐬��:��D��
//
// <�J������>
// 2022/10/13 �쐬
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomLens : MonoBehaviour
{
    [Header("�J�����t���O")]
    public bool isZoom;
    // �J�����O�t���[���t���O
    private bool isBeforeZoom;
    [Header("�J����")]
    public Camera _camera;
    [Header("�J�����̏����l")]
    public float cameraInit = 5;
    [Header("�������̃Y�[���̒l")]
    public float cameraZoom = 3;
    [Header("�Y�[��������W")]
    public Transform pointZoom;
    [Header("�Y�[�����x")]
    public float speedZoomIn = 0.15f;

    // �Y�[���̕�Ԓl
    [System.NonSerialized]
    public float valueZoomLerp;
    // canvas
    private Canvas canvasLens;

    private Vector3 cameraInitPos;

    // Start is called before the first frame update
    void Start()
    {
        canvasLens = this.GetComponent<Canvas>();

        cameraInitPos = _camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // �Y�[��
        if (isZoom == true)
        {
            if (isBeforeZoom != isZoom)
            {
                cameraInitPos = _camera.transform.position;
            }
            // canvas�����[���h���W��(�肪�Y�[���ƈꏏ�ɓ����Ȃ��悤�ɂ��邽��)
            canvasLens.renderMode = RenderMode.WorldSpace;

            // �Y�[���̕�Ԓl�̌���
            valueZoomLerp += speedZoomIn * Time.deltaTime;
            if (valueZoomLerp > 1.0f)
            {
                valueZoomLerp = 1.0f;
            }

            _camera.orthographicSize = Mathf.Lerp(cameraInit, cameraZoom, valueZoomLerp);
            _camera.transform.position = Vector3.Lerp(new Vector3(cameraInitPos.x, cameraInitPos.y, _camera.transform.position.z), new Vector3(pointZoom.position.x, pointZoom.position.y, _camera.transform.position.z), valueZoomLerp);
        }
        // �Y�[������
        else
        {
            if (isBeforeZoom != isZoom)
            {
                // canvas����ʌŒ�ɖ߂�
                canvasLens.renderMode = RenderMode.ScreenSpaceCamera;

                //���Z�b�g
                valueZoomLerp = 0;
                _camera.orthographicSize = cameraInit;
                _camera.transform.position = new Vector3(cameraInitPos.x, cameraInitPos.y, _camera.transform.position.z);
            }
        }
        isBeforeZoom = isZoom;
    }
}
