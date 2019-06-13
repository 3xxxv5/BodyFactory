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

    [HideInInspector] public int ikaCoinCount = 0;

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
            print("animator被禁用");
        }
        //Qte检测
        if (checkQte)
        {
            if (Input.GetMouseButtonUp(1))
            {
                pressCount++;
            }
            //Qte成功：
            if (pressCount >= 2) {
                FirstPersonAIO._instance.qteWin = true;//不检测失败
                AudioManager._instance.PlayEffect("qteWin");//音效
                //播放打击动画
                animIndex = 2;     animator.SetInteger("qteAnim", animIndex);
                AnimatorStateInfo a = animator.GetCurrentAnimatorStateInfo(0);
                StartCoroutine(ChangeAnimState(a.length));
                //关闭qte面板
                DisableQtePanel();
                //dragon的反应：－hp、替换贴图、飞走
            }
        }
    }

    public  IEnumerator Qte(float qteTime)
    {
        //开启Qte检测
        qteCanvas.gameObject.SetActive(true);
        checkQte = true;   Time.timeScale = timeScale;//慢动作
        yield return new WaitForSecondsRealtime(qteTime);
        //Qte失败：
        if (!FirstPersonAIO._instance.qteWin)
        {
            AudioManager._instance.PlayEffect("qteLose");//音效
            DisableQtePanel();
            //dragon的反应：死了
            //StartCoroutine(GameManager2._instance.SeaDead(1f, 1f, 1f, level1ReviveTrans));
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

    IEnumerator ChangeAnimState(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        animator.SetInteger("qteAnim", 0);
    }

    private void OnCollisionEnter(Collision col)//碰到了monster的碰撞盒
    {
        if (checkQte) return;
        if (ShootManager._instance.canChange2Battery && col.gameObject.layer == LayerMask.NameToLayer("battery"))
        {
            ShootManager._instance.Change2BatteryView();
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("monster"))
        {
            //眩晕特效
            GameObject spark = Instantiate(dizzySpark, sparkTrans.position, dizzySpark.transform.rotation);
            spark.transform.SetParent(transform); Destroy(spark, 3f);
        }
        switch (col.gameObject.tag)
        {
            case "Sea":
                switch (GameManager2._instance.levelNow)
                {
                    case GameManager2.LevelNow.isLevel1:
                    case GameManager2.LevelNow.isLevel2:
                        StartCoroutine(GameManager2._instance.SeaDead(1f, 1f, 1f, level1ReviveTrans));
                        break;
                    case GameManager2.LevelNow.isLevel3:
                        StartCoroutine(GameManager2._instance.SeaDead(1f, 1f, 1f, seaReviveTrans));
                        break;
                }             
                break;
            case "coin":
                int index = Random.Range(1, 3);
                AudioManager._instance.PlayEffect("coin" + index.ToString());                //随机播放几种音效
                ikaCoinCount++;
                Level2UIManager._instance.SetCoinText(ikaCoinCount);
                GameObject coinEffect = Instantiate(Resources.Load<GameObject>("Prefabs/" + "coinEffect"), col.transform.position, Quaternion.identity);
                coinEffect.transform.SetParent(transform);
                Destroy(coinEffect, 1f);
                Destroy(col.gameObject);
                break;
        }
    }


}
