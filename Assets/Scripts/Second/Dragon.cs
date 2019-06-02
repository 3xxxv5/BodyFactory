using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public static Dragon _instance { get; private set; }
    public int lifeBase = 3;
    [HideInInspector] public int life = 0;
    bool startDisslove = false;
    float index = 1;
    public bool attackDragon = false;
    enum DargonState
    {
        dargonIdle,
        dargonAttacked,
        dargonLightning,
        dargonDead
    }
    DargonState dragonState;
    Animator animator;
    private void Awake()
    {
        _instance = this;
        life = lifeBase;
    }
    // Start is called before the first frame update
    void Start()
    {
        dragonState = DargonState.dargonIdle;
        animator = transform.GetComponent<Animator>();
        animator.SetInteger("dragonState",(int)dragonState);
    }

    // Update is called once per frame
    void Update()
    {
        if (startDisslove)
        {            
            index = Mathf.Lerp(index,0,Time.deltaTime);
            transform.GetComponent<Renderer>().material.SetFloat("_dissolveAmount", index);
        }

    }
    public void ReduceLife(int num)
    {
        life -= num;
        dragonState = DargonState.dargonAttacked;
        animator.SetInteger("dragonState", (int)dragonState);
    }
    public void SpawnLightning()
    {
        dragonState = DargonState.dargonLightning;
        animator.SetInteger("dragonState", (int)dragonState);
    }
    public void BeAttacked(Vector3 pos)
    {
        print("击中龙了！"); AudioManager._instance.PlayEffect("gun");
    
        if (life <= 0)
        {       //龙爆炸的大特效
            Destroy(Instantiate(Resources.Load<GameObject>("Prefabs/dragonExplode"), transform.position, Quaternion.identity), 10f);
            Destroy(gameObject, 3f);
        }
   

    }
}
