using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureAnim : MonoBehaviour
{
    LineRenderer myRender;

    // Start is called before the first frame update
    void Start()
    {
        myRender = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
            myRender.material.SetFloat("_ScaleX", myRender.GetPosition(1).y);
    }
}
