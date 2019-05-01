using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitOneCol : MonoBehaviour
{
    void StopLifeAnim()
    {
        GameManager2._instance.lifeAnim.SetBool("shake", false);
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("monster"))
        {
            GameManager2._instance.level1Hp -= 1;
            GameManager2._instance.lifeAnim.SetBool("shake",true);
            Invoke("StopLifeAnim",1f);
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
