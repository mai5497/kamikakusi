using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxNormal : MonoBehaviour
{
    private SpriteRenderer sr;  // �ς̃X�v���C�g�����_���[
    private float Alpha;    // �ς̃A���t�@�l

    private const float appearTime = 1.5f;  // �A���t�@�l���オ�肫��܂ł̎���


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
