using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WuZei : MonoBehaviour
{
    public static WuZei _instance { get; private set; }
    //声音
    AudioSource audioSource;
    //爆炸
    Monster monster;
    GameObject dizzySpark;
    public float radius = 5.0F;
    public float power = 1000;
    public Transform sparkTrans;
    public float boomDistance = 0.5f;
    //qte
    public float timeScale = 0.2f;
    public float qteTime = 3f;
    public CanvasGroup qteCanvas;
    bool checkQte = false;
    int pressCount = 0;
    int animIndex = 0;
    Animator animator;
    bool checkQteTrigger = true;
    void Awake()
    {
        _instance = this;      
        audioSource = GetComponent<AudioSource>();        
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
            if (Input.GetMouseButtonUp(1))
            {
                pressCount++;
            }
            if (pressCount >= 2) {
                FirstPersonAIO._instance.qteWin = true;//不再进行失败检测
                AudioManager._instance.PlayEffect("tower");//音效
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
        if (GameManager2._instance.canChange2Battery&& col.gameObject.layer == LayerMask.NameToLayer("battery"))
        {
            print("射中了炮台");
            GameManager2._instance.Change2BatteryView();
        }
        if (checkQte) return;
        if (col.gameObject.layer==LayerMask.NameToLayer("monster"))
        {
            Boom(col.transform);
        }

    }

    public  IEnumerator Qte(float qteTime)
    {
        qteCanvas.gameObject.SetActive(true);
        checkQte = true;   Time.timeScale = timeScale;//慢动作
        yield return new WaitForSecondsRealtime(qteTime);
        if (!FirstPersonAIO._instance.qteWin) DisableQtePanel();
    }
    void DisableQtePanel()
    {
        qteCanvas.gameObject.SetActive(false);
        checkQte = false; Time.timeScale = 1;//恢复时间
        pressCount = 0;
        checkQteTrigger = true;
        //此时不应该检测qte呀
        FirstPersonAIO._instance.hasQte = false;
    }

    void Boom(Transform colTrans)
    {
        if (!FirstPersonAIO._instance.canTransfer) return;
        if (colTrans == null) return;
        SpawnEffects(colTrans);
        SpawnChips(colTrans);
    }
    void SpawnEffects(Transform colTrans)
    {
        audioSource.PlayOneShot(GameManager2._instance.gunClip, GameManager2._instance.Volume / 10);//音效
        Destroy(Instantiate(GameManager2._instance.fire, colTrans.position+transform.forward* boomDistance, Quaternion.identity), 5);//爆炸粒子特效
        //眩晕特效
        GameObject spark = Instantiate(dizzySpark, sparkTrans.position, dizzySpark.transform.rotation);
        spark.transform.SetParent(transform); Destroy(spark, 3f);
    }
    void SpawnChips(Transform colTrans)
    {
        //删掉食物本体
        Destroy(colTrans.gameObject);
        //生成食物碎块        
        monster =colTrans.gameObject.GetComponent<Monster>();
        GameObject chips = Instantiate(monster.chips, colTrans.position + transform.forward * boomDistance, Quaternion.identity);
        Destroy(chips, 3);
        //向碎块添加爆炸力
        Vector3 explosionPos = chips.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);//对爆炸点半径内的collider造成影响
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)  rb.GetComponent<Rigidbody>().AddExplosionForce(power, explosionPos, radius, 3.0F);
            if (hit.tag == "fragment")  Destroy(hit.gameObject, 0.5f);
        }
    }
}
