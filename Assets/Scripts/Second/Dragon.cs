using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public int lifeBase = 3;
    [HideInInspector] public int life = 0;
    bool startDisslove = false;
    float index = 1;
    public bool attackDragon = false;
    public Renderer m_renderer;
    Material[] mats;
    public GameObject attackEffect;
    public GameObject lightningEffect;
    enum DargonState
    {
        dargonIdle,
        dargonAttacked,
        dargonLightning,
        dargonDead
    }
    DargonState dragonState;
    Animator animator;

    void Start()
    {
        //life
        life = lifeBase;
        //mats
        mats = m_renderer.sharedMaterials;
        SetAllMatParams(1);
        //anim
        dragonState = DargonState.dargonIdle;
        animator = transform.GetComponent<Animator>();
        animator.SetInteger("dragonState", (int)dragonState);
    }
    void SetAllMatParams(float val)
    {
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetFloat(MainContainer.bloodTail, val);
            mats[i].SetFloat(MainContainer.bloodHead, val);
            mats[i].SetFloat(MainContainer.bloodChest, val);
            mats[i].SetFloat(MainContainer.head, val);
            mats[i].SetFloat(MainContainer.tail, val);
            mats[i].SetFloat(MainContainer.back, val);
        }
    }
    void SetMatParam(string param,float val)
    {
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetFloat(param, val);
        }
    }

    void Update()
    {
        if (startDisslove)
        {
            index = Mathf.Lerp(index, 0, Time.deltaTime);
            transform.GetComponent<Renderer>().material.SetFloat("_dissolveAmount", index);
        }
    }
    public void CollideBlood(int reduceLife, DargonBloodState dargonBloodState)
    {
        //生命值计数
        life -= reduceLife;
        //没死：只是受伤
        if (life > 0)
        {
            //切换动画
            dragonState = DargonState.dargonAttacked;
            animator.SetInteger("dragonState", (int)dragonState);
            //播放音效
            AudioManager._instance.PlayEffect("blood");
            //生成爆炸特效
            GameObject effect = Instantiate(attackEffect, transform.position, Quaternion.identity);
            effect.transform.SetParent(WuZei._instance.transform);
            Destroy(effect, 2f);
            //调整材质参数
            switch (dargonBloodState)
            {
                case DargonBloodState.bloodChest:
                    SetMatParam(MainContainer.bloodChest, 0);
                    break;
                case DargonBloodState.bloodHead:
                    SetMatParam(MainContainer.bloodHead, 0);
                    break;
                case DargonBloodState.bloodTail:
                    SetMatParam(MainContainer.bloodTail, 0);
                    break;
            }
        }
        //死了
        else
        {
            //切换动画
            dragonState = DargonState.dargonDead;
            animator.SetInteger("dragonState", (int)dragonState);
            //播放音效
            AudioManager._instance.PlayEffect("blood");
            //生成爆炸特效
            GameObject effect = Instantiate(attackEffect, transform.position, Quaternion.identity);
            effect.transform.SetParent(WuZei._instance.transform);
            Destroy(effect, 2f);
            //调整材质参数
            SetAllMatParams(0);
        }        
    }
    public void CollideElectric()
    {
        //切换动画
        dragonState = DargonState.dargonLightning;
        animator.SetInteger("dragonState", (int)dragonState);
        //播放音效
        AudioManager._instance.PlayEffect("dianliu");
        //生成闪电特效
        GameObject effect = Instantiate(lightningEffect, transform.position, Quaternion.identity);
        effect.transform.SetParent(WuZei._instance.transform);
        Destroy(effect, 2f);
        //死掉：需要判断当前是第几关，再决定在哪里复活
        switch (GameManager2._instance.levelNow)
        {
            case GameManager2.LevelNow.isLevel1:
            case GameManager2.LevelNow.isLevel2:
                StartCoroutine(GameManager2._instance.SeaDead(3f, 1f, 1f, WuZei._instance.level1ReviveTrans));
                break;
            case GameManager2.LevelNow.isLevel3:
                StartCoroutine(GameManager2._instance.SeaDead(3f, 1f, 1f, WuZei._instance.seaReviveTrans));
                break;
        }       
    }

}
