using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractRayEmitter : RayEmitter
{
    [HideInInspector]
    public Vector3[] dirs;
    [HideInInspector]
    public  Vector3[] endPoints;
    [HideInInspector]
    public int index = 0;

    void Start () {
        endPoints = new Vector3[4];
        shootPoint = Instantiate(Resources.Load<GameObject>("Prefabs/shootPoint"));
        shootPoint.gameObject.SetActive(false);
    }	

    protected override void CreateRay()
    {
        startPoint = transform.parent.position;
        lineRenderer.SetPosition(0, startPoint);
        for (int j = 0; j < 4; j++)
        {
            endPoints[j] = startPoint + 1000 * dirs[j];
        }        
        Ray ray = new Ray(startPoint, dirs[index]);
        RaycastHit hit;       
        bool isHit = Physics.Raycast(ray, out hit, 1000, layerMask); //不需要禁用自己的碰撞盒
        if ( ! isHit)
        {
            lineRenderer.SetPosition(1, endPoints[index]);
            EmptyLastHit();
            shootPoint.gameObject.SetActive(false);
        }
        else
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("sphereBoard"))
            {
                endPoints[index] = hit.point;
            }
            else
            {
                endPoints[index] = hit.transform.position;
            }

            endPoints[index] = hit.transform.position;
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
