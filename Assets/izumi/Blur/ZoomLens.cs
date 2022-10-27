//=============================================================================
//
// 注視時のズーム処理
//
// 作成日:2022/10/13
// 作成者:泉優樹
//
// <開発履歴>
// 2022/10/13 作成
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomLens : MonoBehaviour
{
    [Header("カメラフラグ")]
    public bool isZoom;
    // カメラ前フレームフラグ
    private bool isBeforeZoom;
    [Header("カメラ")]
    private Camera _camera;
    [Header("多重スクロールカメラ")]
    private List<Camera> _cameraMultList = new List<Camera>();
    [Header("カメラの初期値")]
    public float cameraInit = 5;
    [Header("注視時のズームの値")]
    public float cameraZoom = 3;
    [Header("多重スクロールカメラのズームの値")]
    public float _cameraMultZoom = 5;
    [Header("ズームする座標")]
    public Transform pointZoom;
    [Header("ズーム速度")]
    public float speedZoomIn = 0.15f;

    // ズームの補間値
    [System.NonSerialized]
    public float valueZoomLerp;
    // canvas
    private Canvas canvasLens;

    private Vector3 cameraInitPos;
    private List<Vector3> cameraMultInitPosList = new List<Vector3>();

    // カメラが見つからなかった
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

        // ズーム
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
            // canvasをワールド座標に(手がズームと一緒に動かないようにするため)
            canvasLens.renderMode = RenderMode.WorldSpace;

            // ズームの補間値の決定
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
        // ズーム解除
        else
        {
            if (isBeforeZoom != isZoom)
            {
                // canvasを画面固定に戻す
                canvasLens.renderMode = RenderMode.ScreenSpaceCamera;

                //リセット
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
