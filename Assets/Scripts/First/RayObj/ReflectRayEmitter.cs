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
        shootPoint = Instantiate(Resources.Load<GameObject>("Prefabs/shootPoint"));
        shootPoint.gameObject.SetActive(false);
    }
    protected override void CreateRay()
    {
        startPoint = transform.parent.position;
        lineRenderer.SetPosition(0, startPoint);

        for (int i = 0; i < endPoints.Length; i++)
        {
            endPoints[i] = startPoint + dirs[i] * 1000;
        }
        Ray ray = new Ray(startPoint, dirs[index]);
        RaycastHit hit;
        transform.parent.GetComponent<Collider>().enabled = false;
        bool isHit = Physics.Raycast(ray, out hit, 1000, layerMask);
        transform.parent.GetComponent<Collider>().enabled = true;
        if (!isHit)
        {
            lineRenderer.SetPosition(1, endPoints[index]);
            EmptyLastHit();
            shootPoint.gameObject.SetActive(false);
        }
        else
        {
            endPoints[index] = hit.point;
            lineRenderer.SetPosition(1, endPoints[index]);
            UpdateLastHit(hit);
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("sphereBoard"))
            {
                shootPoint.gameObject.SetActive(true);
                shootPoint.transform.position = hit.point;
            }
        }
    }
}
