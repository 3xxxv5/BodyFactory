using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBall : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 target;
    float moveSpeed = 1f;
    void Start()
    {
        target = FirstPersonAIO._instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("wuzei"))
        {            
            StartCoroutine(GameManager2._instance.SeaDead(3f, 1f, 1f, WuZei._instance.level1ReviveTrans));
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag =="ballVanishTrigger")
        {      
            Destroy(gameObject);
        }
    }
    void MoveToTarget()
    {
        transform.Translate((target-transform.position)*moveSpeed*Time.deltaTime);
    }
}
