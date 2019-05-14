using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WuZei : MonoBehaviour
{
    public static WuZei _instance { get; private set; }

    //爆炸
    GameObject dizzySpark;
    public Transform sparkTrans;//眩晕特效的位置
    //qte
    public float timeScale = 0.2f;
    public float qteTime = 3f;
    public CanvasGroup qteCanvas;
    bool checkQte = false;
    int pressCount = 0;
    int animIndex = 0;
    Animator animator;
    bool checkQteTrigger = true;
    bool hasPlayQteAudio = false;
    public Transform level1ReviveTrans;
    public Transform seaReviveTrans;
    void Awake()
    {
        _instance = this;            
        dizzySpark = Resources.Load<GameObject>("Prefabs/dizzySpark");
        if (sparkTrans == null) print("sparkTrans为空，设置在乌贼头上");
        qteCanvas.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        //游戏结束，乌贼动画不再播放
        if (FirstPersonAIO._instance.gameOver)
        {
            animator.enabled = false;
        }
        //在qte时间内，检测组合键的按下
        if (checkQte)
        {
            //if (!hasPlayQteAudio)
            //{
            //    AudioManager._instance.PlayEffect("qteTime");//音效
            //    hasPlayQteAudio = true;
            //}
            if (Input.GetMouseButtonUp(1))
            {
                pressCount++;
            }
            if (pressCount >= 2) {
                FirstPersonAIO._instance.qteWin = true;//不再进行失败检测
                AudioManager._instance.PlayEffect("qteWin");//音效
                //播放打击动画
                animIndex = Random.Range(1, 4);
                animator.SetInteger("qteAnim", animIndex);
                AnimatorStateInfo a = animator.GetCurrentAnimatorStateInfo(0);
                StartCoroutine(ChangeAnimState(a.length));
                //关闭qte面板
                DisableQtePanel();
            }
        }
    }
    IEnumerator ChangeAnimState(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        animator.SetInteger("qteAnim", 0);
    }
    private void OnCollisionEnter(Collision col)//碰到了monster的碰撞盒
    {
        if (checkQte) return;
        if (GameManager2._instance.canChange2Battery&& col.gameObject.layer == LayerMask.NameToLayer("battery"))
        {
            GameManager2._instance.Change2BatteryView();
        }
        if (col.gameObject.layer==LayerMask.NameToLayer("monster"))
        {
            //眩晕特效
            GameObject spark = Instantiate(dizzySpark, sparkTrans.position, dizzySpark.transform.rotation);
            spark.transform.SetParent(transform); Destroy(spark, 3f);
        }
        if (col.gameObject.tag.Equals("sea"))
        {
            StartCoroutine(GameManager2._instance.SeaDead(1f,1f,1f,seaReviveTrans));
        }
        if (col.gameObject.tag.Equals("level1Sea"))
        {
            StartCoroutine(GameManager2._instance.SeaDead(1f, 1f, 1f, level1ReviveTrans));
        }
        if (col.gameObject.tag.Equals("level2Sea"))
        {
            StartCoroutine(GameManager2._instance.SeaDead(1f, 1f, 1f, seaReviveTrans));
        }
        if (col.gameObject.tag.Equals("electric"))
        {
            AudioManager._instance.PlayEffect("dianliu");
            print("被电击了");
            GameManager2._instance.SpawnSpecialEffects("lightning", transform.position + transform.forward * 3, 5f);
            StartCoroutine(GameManager2._instance.SeaDead(3f, 1f, 1f,seaReviveTrans));
        }
        if (col.gameObject.tag.Equals("dragon"))
        {
            AudioManager._instance.PlayEffect("dianliu");
            GameManager2._instance.SpawnSpecialEffects("fire", transform.position, 5f);
            Dragon._instance.BeAttacked(transform.position);
        }
    }

    public  IEnumerator Qte(float qteTime)
    {
        qteCanvas.gameObject.SetActive(true);
        checkQte = true;   Time.timeScale = timeScale;//慢动作
        yield return new WaitForSecondsRealtime(qteTime);
        AudioManager._instance.PlayEffect("");//音效
        if (!FirstPersonAIO._instance.qteWin)
        {
            AudioManager._instance.PlayEffect("qteLose");//音效
            DisableQtePanel();
        }

    }
    void DisableQtePanel()
    {
        qteCanvas.gameObject.SetActive(false);
        checkQte = false; Time.timeScale = 1;//恢复时间
        hasPlayQteAudio = false;
        pressCount = 0;
        checkQteTrigger = true;
        //此时不应该检测qte呀
        FirstPersonAIO._instance.hasQte = false;
    }



}
