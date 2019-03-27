using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WuZei : MonoBehaviour
{
    //声音
    AudioClip gunClip;
    float Volume = 5f;
    AudioSource audioSource;
    //爆炸
    GameObject fire;
    Monster monster;
    GameObject dizzySpark;
    public float radius = 5.0F;
    public float power = 1000;
    public Transform sparkTrans;

    void Awake()
    {
        gunClip = Resources.Load<AudioClip>("Sound/Effect/gun");
        audioSource = GetComponent<AudioSource>();
        fire = Resources.Load<GameObject>("Prefabs/fire");       
        dizzySpark = Resources.Load<GameObject>("Prefabs/dizzySpark");
        if (sparkTrans == null) print("sparkTrans为空，设置在乌贼头上");
    }


    void Update()
    {
        if (!FirstPersonAIO.enableCameraMovement)
        {
            GetComponent<Animator>().enabled = false;
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer==LayerMask.NameToLayer("monster"))
        {
            Boom(col.transform);
        }
    }
    void Boom(Transform colTrans)
    {
        if (!FirstPersonAIO.canTransfer) return;
        monster = colTrans.gameObject.GetComponent<Monster>();
        audioSource.PlayOneShot(gunClip, Volume / 10);//音效
        Destroy(Instantiate(fire, colTrans.position, Quaternion.identity), 5);//特效
        GameObject chips = Instantiate(monster.chips, colTrans.position, Quaternion.identity);//生成碎块
        Destroy(colTrans.gameObject);//删掉monster
        //爆炸
        Vector3 explosionPos = chips.transform.position;
        Destroy(chips,3);
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);//对爆炸点半径内的collider造成影响
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
                rb.GetComponent<Rigidbody>().AddExplosionForce(power, explosionPos, radius, 3.0F);
            if (hit.tag == "fragment")
            {
                Destroy(hit.gameObject, 0.5f);
            }
        }
        //眩晕特效
        GameObject spark = Instantiate(dizzySpark, sparkTrans.position, dizzySpark.transform.rotation);
        spark.transform.SetParent(transform);
        Destroy(spark,3f);
        
    }
    
}
