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
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("wuzei"))
        {
            print("触电了");
            dragon.CollideElectric(transform.position);        
        }
    }
}
