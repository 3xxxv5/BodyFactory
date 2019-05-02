using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WuZei : MonoBehaviour
{
    public static WuZei _instance { get; private set; }

    //爆炸
    Monster monster;
    GameObject dizzySpark;
    public float radius = 5.0F;
    public float power = 1000;
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
        if (checkQte) return;
        if (GameManager2._instance.canChange2Battery&& col.gameObject.layer == LayerMask.NameToLayer("battery"))
        {
            GameManager2._instance.Change2BatteryView();
        }
        if (col.gameObject.layer==LayerMask.NameToLayer("monster"))
        {
            Boom(col.transform,col.gameObject.tag);
        }
        if (col.gameObject.tag.Equals("sea"))
        {
            print("撞到海了");
            StartCoroutine(GameManager2._instance.SeaDead(1f,1f,1f));
        }
        if (col.gameObject.tag.Equals("electric"))
        {
            print("被电击了");
            GameManager2._instance.SpawnSpecialEffects("lightning", transform.position + transform.forward * 3, 5f);
            StartCoroutine(GameManager2._instance.SeaDead(3f, 1f, 1f));
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

    void Boom(Transform colTrans,string tag)
    {
        if (!FirstPersonAIO._instance.canTransfer) return;
        if (colTrans == null) return;
        SpawnEffects(colTrans,tag);
        SpawnChips(colTrans);
    }
    void SpawnEffects(Transform colTrans,string tag)
    {
        AudioManager._instance.PlayEffect("gun");
        if (tag.Equals("chocolateFrog"))
        {
            StartCoroutine(GameManager2._instance.ChangeAllDropSpeed());//巧克力蛙buff       
            GameManager2._instance.SpawnSpecialEffects("frogHeart", colTrans.position + transform.forward * GameManager2._instance.boomDistance, 3f);
        }
        else
        {
            GameManager2._instance.SpawnSpecialEffects("fire", colTrans.position + transform.forward * GameManager2._instance.boomDistance, 3f);
        }    
        //眩晕特效
        GameObject spark = Instantiate(dizzySpark, sparkTrans.position, dizzySpark.transform.rotation);
        spark.transform.SetParent(transform); Destroy(spark, 3f);
    }
    void SpawnChips(Transform colTrans)
    {
        Monster monster = colTrans.GetComponent<Monster>();
        monster.ReduceLife(1);
        if (monster.life <1)
        {
            //生成食物碎块       
            GameObject chips = Instantiate(monster.chips, colTrans.position + transform.forward * GameManager2._instance.boomDistance, Quaternion.identity);
            Destroy(chips, 3);
            Destroy(colTrans.gameObject);
            //向碎块添加爆炸力
            Vector3 explosionPos = chips.transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);//对爆炸点半径内的collider造成影响
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null) rb.GetComponent<Rigidbody>().AddExplosionForce(power, explosionPos, radius, 3.0F);
                if (hit.tag == "fragment") Destroy(hit.gameObject, 0.5f);
            }
        }

    }
}
