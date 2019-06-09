using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
    public  float dropSpeed=2f;
    public GameObject chips;
    public GameObject chocoChips;
    public GameObject icingChips;
    protected float trueSpeed;
    public  int lifeBase = 2;
    public  int life = 0;
    HighlightableObject ho;
    float radius = 6;
    float power = 500;
    float rotateSpeed = 30f;
    float explodeDistance = 5f;
    bool canCollide = true;

    void Awake () {
        if (chips == null) chips = Resources.Load<GameObject>("Prefabs/m_donutChips");
        life = lifeBase;
        ho = gameObject.AddComponent<HighlightableObject>();
    }
	

	void Update () {
          Move();
          ChangeColor();
    }
    void ChangeColor()
    {
        if (ho == null) return;        
        switch (life)
        {
            case 3:
                ho.ConstantOn(GameManager2._instance.thirdTimes);
                break;
            case 2:
                ho.ConstantOn(GameManager2._instance.twice);
                break;
            case 1:
                ho.ConstantOn(GameManager2._instance.once);
                break;
        }
    }
    protected void Move()
    {
            trueSpeed = dropSpeed + GameManager2._instance.buffDropSpeed;
            Mathf.Clamp(trueSpeed, 1f, 10);
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, -1, 0), Time.deltaTime * trueSpeed);
            transform.Rotate(Vector3.up,Time.deltaTime*rotateSpeed);
    }

   public  void ReduceLife(int num)
    {
        life -= num;        
    }
    protected void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("bigBullet"))
        {          
            Destroy(gameObject);
        }

    }
    private void OnCollisionEnter(Collision col)
    {
        if (!canCollide) return;
        if (col.gameObject.layer == LayerMask.NameToLayer("wuzei"))
        {
            print("碰到了");
            if (!FirstPersonAIO._instance.canTransfer) return;
            SpawnEffects();//不管是第几次攻击都要生成爆炸特效，不同monster特效可能不同
            switch (life)
            {
                case 3://所谓的糖霜消失
                    if (icingChips != null)
                    {
                        SpawnChips(icingChips);
                        transform.Find("icing").gameObject.SetActive(false);
                    }
                    break;
                case 2://所谓的巧克力外衣消失
                    if (chocoChips != null)
                    {
                        SpawnChips(chocoChips);
                        transform.Find("chocolate").gameObject.SetActive(false);
                    }
                    break;
                case 1:
                    SpawnChips(chips);
                    Destroy(gameObject);
                    break;
            }
            canCollide = false;//碰撞一次之后先禁用1s
            StartCoroutine(waitToCollide(1));
        }

    }
    void SpawnEffects()
    {
        AudioManager._instance.PlayEffect("gun");
        if (gameObject.tag.Equals("chocolateFrog"))
        {
            StartCoroutine(GameManager2._instance.ChangeAllDropSpeed());//巧克力蛙buff       
            GameManager2._instance.SpawnSpecialEffects("frogFrost", transform.position, 3f);
        }
        else
        {
            GameManager2._instance.SpawnSpecialEffects("fire", transform.position, 3f);
        }
    }
    void SpawnChips(GameObject m_chips)
    {
        ReduceLife(1);
        //生成食物碎块       
        GameObject myChips = Instantiate(m_chips, transform.position, Quaternion.identity);
        myChips.transform.SetParent(WuZei._instance.transform);
        Destroy(myChips, 1f);
        //向碎块添加爆炸力
        Vector3 explosionPos = myChips.transform.position - WuZei._instance.transform.forward * explodeDistance;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);//对爆炸点半径内的collider造成影响
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null) rb.GetComponent<Rigidbody>().AddExplosionForce(power, explosionPos, radius, -3f);
        }
    }
    IEnumerator waitToCollide(float time)
    {
        yield return new WaitForSeconds(time);
        canCollide = true;
    }
}
