using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitOneCol : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("monster"))
        {
            AudioManager._instance.PlayEffect("foodToWater");//音效
            GameManager2._instance.level1Hp -= 1;           
            AudioManager._instance.PlayEffect("bud");
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
