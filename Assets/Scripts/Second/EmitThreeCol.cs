using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitThreeCol : MonoBehaviour
{

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("monster"))
        {
            GameManager2._instance.level3Hp -= 1;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("monster"))
        {
            Destroy(col.gameObject);
        }
    }
}
