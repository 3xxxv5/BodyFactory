using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myExport : MonoBehaviour
{
    public GameObject meshObject;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] meshObjs = new GameObject[1];
        meshObjs[0] = meshObject;
        //用到动态库WRP_FBXExporter
        FBXExporter.ExportFBX("", "level1", meshObjs, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
