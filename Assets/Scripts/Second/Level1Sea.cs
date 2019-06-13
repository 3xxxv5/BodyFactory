using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Sea : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("monster"))
        {
            AudioManager._instance.PlayEffect("foodToWater");//音效
            GameManager2._instance.level1Hp--;
            AudioManager._instance.PlayEffect("bud");
            GameManager2._instance.level1foodNum--;
            Destroy(col.gameObject);
        }
    }
}
