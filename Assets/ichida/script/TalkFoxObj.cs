using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkFoxObj : MonoBehaviour
{
    private Animator animator;

    private Fox_text _Fox_text;

    private GameObject rendereParent;
    private Renderer[] renderers;

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
        //renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManagerData.nowWorldNo == 0 && SceneManagerData.nowStageNo == 0) {
            if (_Fox_text.count > 4) {
                animator.SetBool("isChange", true);
            }
            if (_Fox_text.isTextFin) {
                for (int i = 0; i < renderers.Length; i++) {
                    renderers[i].material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                }
            }
        } else {
            if (_Fox_text.isTextFin) {
                animator.SetBool("isChange", true);
                for (int i = 0; i < renderers.Length; i++) {
                    renderers[i].material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                }
            }
        }
    }
}
