using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkFoxObj : MonoBehaviour
{
    private Animator animator;

    private Fox_text _Fox_text;

    private GameObject rendereParent;
    private Renderer[] renderers;
    private float alpha;    // 狐のアルファ値
    private const float deleteTime = 5.0f;  // アルファ値が下がりきるまでの時間

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        _Fox_text = GameObject.Find("Text (fox)").GetComponent<Fox_text>();

        rendereParent = transform.Find("Drawables").gameObject;

        renderers = new Renderer[rendereParent.transform.childCount];

        for(int i = 0;i < renderers.Length; i++) {
            renderers[i] = rendereParent.transform.GetChild(i).GetComponent<Renderer>();
        }

        alpha = 1.0f;
        //renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManagerData.nowWorldNo == 0 && SceneManagerData.nowStageNo == 0) {
            if (_Fox_text.count > 5) {
                animator.SetBool("isChange", true);
            }
            if (_Fox_text.isTextFin) {
                FadeOutFox();   // 狐をフェードアウトさせる。(毎フレーム通るようにしてください)
            }
        } else {
            if (_Fox_text.isTextFin) {
                animator.SetBool("isChange", true);
                FadeOutFox();
            }
        }
    }

    private void FadeOutFox() {
        //if (alpha < 0.1) {
        //    return; // アルファ値が下がりきったため下のループに入らない
        //}

        for (int i = 0; i < renderers.Length; i++) {
            alpha -= Time.deltaTime / deleteTime;
            renderers[i].material.color = new Color(1.0f, 1.0f, 1.0f, alpha);
            //renderers[i].material.color = new Color(1.0f, 1.0f, 1.0f, 0);
        }
    }
}
