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
    private Camera _camera;
    [Header("���d�X�N���[���J����")]
    private List<Camera> _cameraMultList = new List<Camera>();
    [Header("�J�����̏����l")]
    public float cameraInit = 5;
    [Header("�������̃Y�[���̒l")]
    public float cameraZoom = 3;
    [Header("���d�X�N���[���J�����̃Y�[���̒l")]
    public float _cameraMultZoom = 5;
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
    private List<Vector3> cameraMultInitPosList = new List<Vector3>();

    // �J������������Ȃ�����
    private bool isFindNotCamera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        isFindNotCamera = true;
        //if (GameObject.Find("CameraForward") != null)
        //{
        //    _cameraMultList.Add(GameObject.Find("CameraForward").GetComponent<Camera>());
        //    _cameraMultList.Add(GameObject.Find("CameraMiddle1").GetComponent<Camera>());
        //    _cameraMultList.Add(GameObject.Find("CameraMiddle2").GetComponent<Camera>());
        //    _cameraMultList.Add(GameObject.Find("CameraMiddle3").GetComponent<Camera>());
        //    _cameraMultList.Add(GameObject.Find("CameraBack").GetComponent<Camera>());
        //    isFindNotCamera = false;
        //}

        canvasLens = this.GetComponent<Canvas>();

        cameraInitPos = _camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFindNotCamera == true)
        {
            if (GameObject.Find("CameraForward") != null)
            {
                _cameraMultList.Add(GameObject.Find("CameraForward").GetComponent<Camera>());
                _cameraMultList.Add(GameObject.Find("CameraMiddle1").GetComponent<Camera>());
                _cameraMultList.Add(GameObject.Find("CameraMiddle2").GetComponent<Camera>());
                _cameraMultList.Add(GameObject.Find("CameraMiddle3").GetComponent<Camera>());
                _cameraMultList.Add(GameObject.Find("CameraBack").GetComponent<Camera>());
                for (int i = 0; i < _cameraMultList.Count; i++)
                {
                    cameraMultInitPosList.Add(_cameraMultList[i].transform.position);
                }
            }
            isFindNotCamera = false;
        }

        // �Y�[��
        if (isZoom == true)
        {
            if (isBeforeZoom != isZoom)
            {
                cameraInitPos = _camera.transform.position;
                for (int i = 0; i < _cameraMultList.Count; i++)
                {
                    cameraMultInitPosList[i] = (_cameraMultList[i].transform.position);
                }
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

            for (int i = 0; i < _cameraMultList.Count; i++)
            {
                _cameraMultList[i].transform.position = Vector3.Lerp(cameraMultInitPosList[i], new Vector3(pointZoom.position.x + (cameraMultInitPosList[i].x - cameraInitPos.x), pointZoom.position.y + (cameraMultInitPosList[i].y - cameraInitPos.y), _cameraMultList[i].transform.position.z + _cameraMultZoom), valueZoomLerp);
            }
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

                for (int i = 0; i < _cameraMultList.Count; i++)
                {
                    _cameraMultList[i].transform.position = cameraMultInitPosList[i];
                }
            }
        }
        isBeforeZoom = isZoom;
    }
}
