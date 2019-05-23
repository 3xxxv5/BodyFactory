using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachTower : MonoBehaviour
{
    protected Transform tipBoard;
    [HideInInspector] public RayEmitter rayEmitter;
    [HideInInspector]
    public bool beHited = false;
    public bool hasPlayEffect = false;
    [HideInInspector] public Animator anim;
    public ReflectFairy reflectFairy;
    public RefractFairy refractFairy;
    bool hasTeach = false;
    [HideInInspector]public  HighlightableObject ho;
    public Color color;
    void Awake()
    {
        Init();
    } 
    public  void Init()
    {
        tipBoard = transform.Find("tipBoard");
        tipBoard.gameObject.SetActive(false);
        rayEmitter = GetComponentInChildren<RayEmitter>();
        rayEmitter.startPoint = transform.position;
        anim = transform.Find("flowerTower").GetComponentInChildren<Animator>();
        ho = GetComponent<HighlightableObject>();
    }
    // Update is called once per frame
    void Update()
    {
        PlayerLight();
    }
    public void PlayerLight()
    {
        //主角点亮
        if (!hasTeach)
        {
            ho.ConstantOn(color);
            if (Input.GetKeyUp(KeyCode.E))
            {
                AudioManager._instance.PlayEffect("tower");
                tipBoard.gameObject.SetActive(false);
                rayEmitter.lineRenderer.enabled = true;
                anim.SetTrigger("lightTower");
                reflectFairy.canFollow = true;
                refractFairy.canFollow = true;
                //花的轮廓光消失
                ho.ConstantOff();
                hasTeach = true;
            }
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
