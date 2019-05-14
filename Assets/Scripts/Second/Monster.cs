using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
    public  float dropSpeed=2f;
    public GameObject chips;
    protected float trueSpeed;
    public  int lifeBase = 2;
    [HideInInspector]public  int life = 0;
    HighlightableObject ho;
    public float radius = 5.0F;
    public float power = 1000;
    float rotateSpeed = 10f;
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
        switch (life)
        {
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
        if (col.gameObject.layer == LayerMask.NameToLayer("wuzei"))
        {
            if (!FirstPersonAIO._instance.canTransfer) return;
            SpawnEffects();
            SpawnChips();
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
    void SpawnChips()
    {
        ReduceLife(1);
        if (life < 1)
        {
            //生成食物碎块       
            GameObject myChips = Instantiate(chips, transform.position + transform.forward * GameManager2._instance.boomDistance, Quaternion.identity);
            Destroy(myChips, 3);
            //向碎块添加爆炸力
            Vector3 explosionPos = myChips.transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);//对爆炸点半径内的collider造成影响
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null) rb.GetComponent<Rigidbody>().AddExplosionForce(power, explosionPos, radius, 3.0F);
                if (hit.tag == "fragment") Destroy(hit.gameObject, 0.5f);
            }
            Destroy(gameObject);
        }

    }
}
