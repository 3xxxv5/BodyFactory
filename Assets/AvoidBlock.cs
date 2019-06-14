using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidBlock : MonoBehaviour
{    
    //观察目标  
    public Transform Target;
    public LayerMask layermasks;
    //遮挡住主角的物体
    public List<Transform> lastHitObjs;
    public List<Transform> hitObjs;

    void Start()
    {
        lastHitObjs = new List<Transform>();
        hitObjs = new List<Transform>();
    }


    void Update()
    {
        AvoidViewBlock();
    }
    void AvoidViewBlock()
    {
        Vector3 ori = Target.position + Vector3.up * 0.2f;
        Vector3 ori2 = Target.position - Vector3.up * 0.1f;
        Vector3 dir = (transform.position - Target.position).normalized;

        Debug.DrawRay(ori, dir);
        Debug.DrawRay(ori2, dir);
        RaycastHit[] raycastHits = Physics.RaycastAll(ori, dir, Mathf.Infinity, layermasks);
        RaycastHit[] raycastHits2 = Physics.RaycastAll(ori2, dir, Mathf.Infinity, layermasks);

        hitObjs.Clear();

        for (int i = 0; i < raycastHits.Length; i++)
        {
            if (raycastHits[i].transform.Equals(Target)) continue;
            Transform trans = raycastHits[i].transform;
            if (trans.GetComponent<Renderer>() == null) continue;
            hitObjs.Add(trans);//把除了主角本身以外碰撞到的物体加入到List中              
            InActivateGo(trans);
        }

        for (int i = 0; i < raycastHits2.Length; i++)
        {
            if (raycastHits2[i].transform.Equals(Target)) continue;
            Transform trans = raycastHits2[i].transform;
            if (trans.GetComponent<Renderer>() == null) continue;
            if (!hitObjs.Contains(trans))
            {
                hitObjs.Add(trans);//把除了主角本身以外碰撞到的物体加入到List中              
                InActivateGo(trans);
            }
        }

        for (int j = 0; j < hitObjs.Count; j++)
        {
            if (lastHitObjs.Contains(hitObjs[j]))
            {
                lastHitObjs.RemoveAt(lastHitObjs.IndexOf(hitObjs[j]));
            }
        }

        for (int i = 0; i < lastHitObjs.Count; i++)
        {
            ActivateGo(lastHitObjs[i]);
        }
        for (int i = 0; i < hitObjs.Count; i++)
        {
            lastHitObjs.Add(hitObjs[i]);//这次的物体移到上次被碰到的List当中去
        }
    }


    void InActivateGo(Transform trans)
    {
        Renderer[] renderers = trans.GetComponentsInChildren<Renderer>();
        foreach (var i in renderers) i.enabled = false;                                                  
    }
    void ActivateGo(Transform trans)
    {
        if (trans == null) return;
        Renderer[] renderers = trans.GetComponentsInChildren<Renderer>();
        foreach (var i in renderers) i.enabled = true;       
    }
}
