using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaskOnOff : MonoBehaviour
{
    private SpriteMask sm;
    // Start is called before the first frame update
    void Start()
    {
        sm = this.GetComponent<SpriteMask>();
    }

    private void Update() {
        if (CPData.isLens) {
            sm.enabled = true;
        } else {
            sm.enabled = false;
        }
    }
}
