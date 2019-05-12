using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickColor : MonoBehaviour
{
    public Color hp1;
    public Color hp2;
    public Color hp3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetComponent<HighlightableObject>().ConstantOn(hp1);;
    }
}
