using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主角靠近，出现tipBoard，点击E，点亮光柱（激活lineRenderer组件）
/// </summary>
public class Tower : MonoBehaviour {

    Transform tipBoard;
    RayEmitter rayEmitter;
    [HideInInspector]
    public bool beHited = false;
    public bool hasPlayEffect = false;
    Animator anim;
    void Start () {
        tipBoard = transform.Find("tipBoard");
        tipBoard.gameObject.SetActive(false);
        rayEmitter = GetComponentInChildren<RayEmitter>();
        rayEmitter.startPoint = transform.position;
        anim = GetComponentInChildren<Animator>();
    }
	

	void Update () {
        //主角点亮
        if (tipBoard.gameObject.activeSelf)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                AudioManager._instance.PlayEffect("tower");
                tipBoard.gameObject.SetActive(false);
                rayEmitter.lineRenderer.enabled = true;
                anim.SetTrigger("lightTower");
            }
        }
        //别的光线点亮
        if (beHited)
        {
            if (!hasPlayEffect)
            {
                AudioManager._instance.PlayEffect("tower");
                hasPlayEffect = true;
            }
            rayEmitter.lineRenderer.enabled = true;
            anim.SetTrigger("lightTower");
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
