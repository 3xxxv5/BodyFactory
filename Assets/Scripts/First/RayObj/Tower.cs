using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主角靠近，出现tipBoard，点击E，点亮光柱（激活lineRenderer组件）
/// </summary>
public class Tower : MonoBehaviour {

    protected Transform tipBoard;
    [HideInInspector] public  RayEmitter rayEmitter;
    [HideInInspector]
    public bool beHited = false;
    public bool hasPlayEffect = false;
    [HideInInspector] public  Animator anim;
    void Awake () {
        Init();
    }
	
    public virtual void Init()
    {
        tipBoard = transform.Find("tipBoard");
        tipBoard.gameObject.SetActive(false);
        rayEmitter = GetComponentInChildren<RayEmitter>();
        rayEmitter.startPoint = transform.position;
        anim = transform.Find("flowerTower").GetComponentInChildren<Animator>();
    }
	void Update () {

        PlayerLight();
        OtherLight();
    }

    public virtual void PlayerLight()
    {
        //主角点亮
        if (tipBoard.gameObject.activeSelf)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                AudioManager._instance.PlayEffect("tower");
                tipBoard.gameObject.SetActive(false);
                rayEmitter.lineRenderer.enabled = true;
                anim.SetBool("lightTower",true);
            }
        }
    }
    public virtual void OtherLight()
    {
        //别的光线点亮
        if (beHited)
        {
            if (!hasPlayEffect)
            {
                AudioManager._instance.PlayEffect("tower");
                hasPlayEffect = true;
            }
            rayEmitter.lineRenderer.enabled = true;
            anim.SetBool("lightTower", true);
        }
    }
   
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("Player"))
        {
            if (!rayEmitter.lineRenderer.enabled)
            {
                tipBoard.gameObject.SetActive(true);

            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag.Equals("Player"))
        {
            tipBoard.gameObject.SetActive(false);
        }
    }


}
