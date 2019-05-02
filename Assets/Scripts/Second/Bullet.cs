using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("wall"))
        {
            GameManager2._instance.SpawnSpecialEffects("fire", transform.position, 5f);
        }
    }
}
