using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBall : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]public  Vector3 target;
    float moveSpeed = 5f;
    void Start()
    {
        target = FirstPersonAIO._instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("wuzei"))
        {
            StartCoroutine(GameManager2._instance.SeaDead(1f, 1f, 0f, WuZei._instance.level1ReviveTrans));
            Destroy(gameObject,2.3f);
        }
        if (col.gameObject.tag =="ballVanishTrigger")
        {      
            Destroy(gameObject);
        }
    }
    void MoveToTarget()
    {
        transform.Translate((target-transform.position).normalized*moveSpeed*Time.deltaTime);
    }
}
