using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayEmitter : MonoBehaviour {
    [HideInInspector]
    public  LineRenderer lineRenderer;
    [HideInInspector]
    public  Vector3 startPoint;
    float endY;
    bool hasAddBud = false;
    bool hasReduceBud = false;
    bool hasAddReflect = false;
    bool hasReduceReflect = false;
    bool hasAddRefract = false;
    bool hasReduceRefract = false;
    protected LayerMask layerMask;
    protected Transform lastHit;

    void Awake () {        
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = lineRenderer.endWidth = 0.2f;
        lineRenderer.material = Resources.Load<Material>("Materials/TexAnim");
        layerMask = LayerMask.GetMask("tower", "reflectObj", "refractObj", "bud","sphereBoard");
        lastHit = null;
        gameObject.AddComponent<TextureAnim>();
    }
	

	void Update () {
        if (lineRenderer.enabled)
        {
            CreateRay();
        }
        else
        {
            if (lastHit != null)
            {
                TellHitedObj(lastHit, false);
                lastHit = null;
            }
        }
    }

    protected virtual void CreateRay()
    {
        //确定射线起点
        startPoint = transform.parent.position;
        Ray ray = new Ray(startPoint, Vector3.up);
        RaycastHit hitInfo;
        //射线不要把父物体算进去
        transform.parent.GetComponent<Collider>().enabled = false;
        bool isHit = Physics.Raycast(ray, out hitInfo, Mathf.Infinity,layerMask);
        transform.parent.GetComponent<Collider>().enabled = true;

        if (isHit)//射中了且在选择模式
        {
            // && hitInfo.collider.gameObject.GetComponent<Fairy>() != null && hitInfo.collider.gameObject.GetComponent<Fairy>().isPicked)
            //射中了某个物体
            endY = hitInfo.transform.position.y;
            UpdateLastHit(hitInfo);
        }
        else
        {
            endY = 1000f;
            EmptyLastHit();
        }

        //画出射线
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, new Vector3(startPoint.x, endY, startPoint.z));       
    }
 
  protected void EmptyLastHit()
    {
        if (lastHit != null)
        {
            TellHitedObj(lastHit, false);
            lastHit = null;
        }
    }

  protected  void UpdateLastHit(RaycastHit hit)
    {      
        if (lastHit == null)
        {
            TellHitedObj(hit.transform, true);
            lastHit = hit.transform;         
        }
        else
        {
            if (lastHit != hit.transform)
            {
                //射线从上一个物体转移
                TellHitedObj(lastHit, false);
                TellHitedObj(hit.transform, true);
                lastHit = hit.transform;
            }
        }
    }
  protected  void TellHitedObj(Transform trans,bool isHited)
    {
        switch (LayerMask.LayerToName(trans.gameObject.layer))
        {
            case "tower":
                Tower tower = trans.GetComponent<Tower>();
                if (tower != null)
                    tower.beHited = isHited;
                break;
            case "reflectObj":
                ReflectFairy reflect = trans.GetComponent<ReflectFairy>();
                if (reflect != null)
                {
                    if (isHited)
                    {
                        if (!hasAddReflect)
                        {
                            AudioManager._instance.PlayEffect("reflect");
                            reflect.hitRay++;
                            hasAddReflect = true;
                            hasReduceReflect = false;
                        }
                    }
                    else
                    {
                        if (!hasReduceReflect)
                        {
                            AudioManager._instance.PlayEffect("refract");
                            reflect.hitRay--;
                            hasReduceReflect = true;
                            hasAddReflect = false;
                        }
                    }        
                }
                break;
            case "refractObj":
                RefractFairy refract = trans.GetComponent<RefractFairy>();
                if (refract != null)
                {
                    if (isHited)
                    {
                        if (!hasAddRefract)
                        {
                            refract.hitRay++;
                            hasAddRefract = true;
                            hasReduceRefract = false;
                        }
                    }
                    else
                    {
                        if (!hasReduceRefract)
                        {
                            refract.hitRay--;
                            hasReduceRefract = true;
                            hasAddRefract = false;
                        }
                    }
                }
                break;
            case "bud":
                Bud bud = trans.GetComponent<Bud>();
                if (bud != null)
                {
                    if (isHited)
                    {
                        if (!hasAddBud)
                        {
                            AudioManager._instance.PlayEffect("bud");
                            bud.nowRayNum++;
                            hasAddBud = true;
                            hasReduceBud = false;
                        }
                    }
                    else
                    {
                        if (!hasReduceBud)
                        {
                            bud.nowRayNum--;
                            hasReduceBud = true;
                            hasAddBud = false;
                        }
                    }                                                  
                }                    
                break;
        }
    }
}
