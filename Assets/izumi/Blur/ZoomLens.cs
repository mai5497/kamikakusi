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
    public Camera _camera;
    [Header("カメラの初期値")]
    public float cameraInit = 5;
    [Header("注視時のズームの値")]
    public float cameraZoom = 3;
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

    // Start is called before the first frame update
    void Start()
    {
        canvasLens = this.GetComponent<Canvas>();

        cameraInitPos = _camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // ズーム
        if (isZoom == true)
        {
            if (isBeforeZoom != isZoom)
            {
                cameraInitPos = _camera.transform.position;
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
            }
        }
        isBeforeZoom = isZoom;
    }
}
