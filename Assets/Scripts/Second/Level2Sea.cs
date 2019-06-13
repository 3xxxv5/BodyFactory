using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Sea : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("monster"))
        {
            GameManager2._instance.level2Hp -= 1;
            AudioManager._instance.PlayEffect("bud");
            GameManager2._instance.level2foodNum--;
            Destroy(col.gameObject);
        }
    }

}
