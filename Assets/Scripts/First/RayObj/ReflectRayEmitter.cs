using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectRayEmitter : RayEmitter {
    [HideInInspector]
    public int dirNum;
    [HideInInspector]
    public  Vector3[] dirs;
    public  Vector3[] endPoints;
    [HideInInspector]
    public  int index = 0;
    
    private void Start()
    {
        endPoints = new Vector3[dirNum];
    }
    protected override void CreateRay()
    {
        startPoint = transform.parent.position;
        lineRenderer.SetPosition(0, startPoint);

        for (int i = 0; i < dirNum; i++)
        {
            endPoints[i] = startPoint + dirs[i] * 1000;
        }
        Ray ray = new Ray(startPoint, dirs[index]);
        RaycastHit hit;
        transform.parent.GetComponent<Collider>().enabled = false;
        bool isHit = Physics.Raycast(ray, out hit, 1000, layerMask);
        transform.parent.GetComponent<Collider>().enabled = true;
        if (! isHit)
        {
            lineRenderer.SetPosition(1, endPoints[index]);
            EmptyLastHit();            
        }
        else
        {
            endPoints[index] = hit.transform.position;
            lineRenderer.SetPosition(1, endPoints[index]);
            UpdateLastHit(hit);
        }
    }
}
