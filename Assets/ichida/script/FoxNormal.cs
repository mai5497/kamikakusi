using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxNormal : MonoBehaviour
{
    private SpriteRenderer sr;  // 狐のスプライトレンダラー
    private float Alpha;    // 狐のアルファ値

    private const float appearTime = 1.5f;  // アルファ値が上がりきるまでの時間


    private bool isDelete;

    private FoxByakko fox;


    // Start is called before the first frame update
    void Start() {
        sr = GetComponent<SpriteRenderer>();
        Alpha = 0.0f;
        sr.color = new Color(1, 1, 1, Alpha);

        fox = GameObject.Find("FoxByakko").GetComponent<FoxByakko>();
    }

    // Update is called once per frame
    void Update() {
        isDelete = fox.GetByakko_delete();

        if (isDelete) {
            Alpha += Time.deltaTime / appearTime;
            sr.color = new Color(1, 1, 1, Alpha);
        }
    }
}
