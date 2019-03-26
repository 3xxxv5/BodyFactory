using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour {//-fr
    private float mSize = 0.0f;
    private bool invert = false;
	// Use this for initialization
	void Start () {
        InvokeRepeating("Scale",0.0f,0.01f);
	}

    // Update is called once per frame
    void Scale() {
        if (mSize > 100.0f || mSize < 0.0f) {
            //CancelInvoke("Scale");
            invert = !invert;
            
        }
        if(invert == false)
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, mSize++);
        else
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, mSize--);
    }
}
