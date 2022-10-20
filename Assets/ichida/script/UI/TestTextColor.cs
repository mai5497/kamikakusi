using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTextColor : MonoBehaviour
{
    private Text text;

    private string _text;

    private string hintmoji = "‚ ";


    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        _text = text.text;

        _text = _text.Replace(hintmoji, "<color=yellow>" + hintmoji + "</color>");
        text.text = _text;

    }

    // Update is called once per frame
    void Update()
    {
    }
}
