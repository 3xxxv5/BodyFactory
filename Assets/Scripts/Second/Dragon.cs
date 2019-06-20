using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    int lifeBase = 6;
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
    [HideInInspector]public bool hasDead = false;
    DargonState dragonState;
    Animator animator;
    public GameObject dragonExplodeEffect;
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
        dragonExplodeEffect.SetActive(false);
    }
    void SetAllMatParams(float val)
    {
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetFloat(MainContainer.bloodTail, val);
            mats[i].SetFloat(MainContainer.bloodHead, val);
            mats[i].SetFloat(MainContainer.bloodChest, val);
            mats[i].SetFloat(MainContainer.bloodFace, val);
            mats[i].SetFloat(MainContainer.bloodRear, val);
            mats[i].SetFloat(MainContainer.bloodBack, val);
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
    public void CollideBlood(int reduceLife, DargonBloodState dargonBloodState,Vector3 pos)
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
            AudioManager._instance.PlayEffect("blood");//电鳗受伤声音
            AudioManager._instance.PlayEffect("dragonHurt");//电鳗受伤哀鸣
            //生成爆炸特效
            GameObject effect = Instantiate(attackEffect, pos, Quaternion.identity);
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
                case DargonBloodState.bloodFace:
                    SetMatParam(MainContainer.bloodFace, 0);
                    break;
                case DargonBloodState.bloodBack:
                    SetMatParam(MainContainer.bloodBack, 0);
                    break;
                case DargonBloodState.bloodRear:
                    SetMatParam(MainContainer.bloodRear, 0);
                    break;
            }
            //第一关，被打一下后，逃走
            if (life == (lifeBase - 1) && GameManager2._instance.levelNow == GameManager2.LevelNow.isLevel1)
            {
                StartCoroutine(DragonManager._instance.Level1Run(1f,1f,0f));
            }
        }
        //死了
        else
        {   //播放音效
            AudioManager._instance.PlayEffect("dragonDead");//电鳗死亡哀鸣
            AudioManager._instance.PlayEffect("blood");//电鳗受伤声音
            //生成爆炸特效
            GameObject effect = Instantiate(attackEffect, pos, Quaternion.identity);
            effect.transform.SetParent(WuZei._instance.transform);
            hasDead = true;
            print("电鳗死了");
        }        
    }
    public void Dead()
    {
        print("执行了死亡切换");
        //调整材质参数
        SetAllMatParams(0);
        //切换动画
        dragonState = DargonState.dargonDead;
        animator.SetInteger("dragonState", (int)dragonState);      
        //生成爆炸特效
        StartCoroutine(WaitToSpawnEffect());
        //死了之后不再发射
        StopCoroutine(DragonEmitManager._instance.SpawnLightningBall());
        StopCoroutine(DragonEmitManager._instance.SpawnCircleBall());     
    }
    IEnumerator WaitToSpawnEffect()
    {
        yield return new WaitForSeconds(2f);
        dragonExplodeEffect.SetActive(true);
        yield return new WaitForSeconds(2f);
        dragonExplodeEffect.SetActive(false);
        dragonExplodeEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        dragonExplodeEffect.SetActive(false);
        dragonExplodeEffect.SetActive(true);
    }
    public void CollideElectric(Vector3 pos)
    {
        //切换动画
        dragonState = DargonState.dargonLightning;
        animator.SetInteger("dragonState", (int)dragonState);
        //播放音效
        AudioManager._instance.PlayEffect("dianliu");
        //生成闪电特效
        GameObject effect = Instantiate(lightningEffect, pos, Quaternion.identity);
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
                StartCoroutine(GameManager2._instance.SeaDead(3f, 1f, 1f, WuZei._instance.level1ReviveTrans));
                break;
        }       
    }

   
}
