using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("monster"))
        {
            AudioManager._instance.PlayEffect("gun");

            if (col.gameObject.tag.Equals("chocolateFrog"))
            {
                StartCoroutine(MonsterManager._instance.ChangeAllDropSpeed());//巧克力蛙buff       
                GameObject effect = Instantiate(Resources.Load<GameObject>("Prefabs/frogFrost"), transform.position, Quaternion.identity);
                Destroy(effect, 2f);
            }
            else
            {
                GameObject effect = Instantiate(Resources.Load<GameObject>("Prefabs/fireworkBlue"), transform.position, Quaternion.identity);
                Destroy(effect, 2f);
            }
            //同归于尽
            Destroy(col.gameObject);
            Destroy(transform.parent.gameObject,0.5f);//停留0.5s
        }
    }
}
