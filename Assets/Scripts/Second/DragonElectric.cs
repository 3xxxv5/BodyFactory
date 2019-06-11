using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonElectric : MonoBehaviour
{
    private Dragon dragon;
    private void Awake()
    {
        dragon = transform.parent.GetComponent<Dragon>();
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("wuzei"))
        {          
            dragon.CollideElectric();        
        }
    }
}
