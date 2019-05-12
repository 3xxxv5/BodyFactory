using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitTwoCol : MonoBehaviour
{
    void StopLifeAnim()
    {

    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("monster"))
        {
            GameManager2._instance.level2Hp -= 1;

            Invoke("StopLifeAnim", 1f);
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
