using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCircleBall : MonoBehaviour
{
    [HideInInspector] public Transform target;
    [HideInInspector] public Transform center;
    float moveSpeed = 5f;

    void Update()
    {
        MoveToTarget();
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "ballVanishTrigger")
        {
            Destroy(gameObject);
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("wuzei"))
        {
            StartCoroutine(GameManager2._instance.SeaDead(1f, 1f, 0f, WuZei._instance.level1ReviveTrans));
            Destroy(gameObject);
        }
    }
    void MoveToTarget()
    {
        if (target != null)
        {
            transform.Translate((target.position - center.position).normalized * moveSpeed * Time.deltaTime);
        }
    }
     
}
