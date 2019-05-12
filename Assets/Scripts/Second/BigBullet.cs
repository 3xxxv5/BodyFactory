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
                StartCoroutine(GameManager2._instance.ChangeAllDropSpeed());//巧克力蛙buff       
                GameManager2._instance.SpawnSpecialEffects("frogFrost", transform.position,0.5f);
            }
            else
            {
                GameManager2._instance.SpawnSpecialEffects("fireworkBlue", transform.position, 0.5f);
            }
            //同归于尽
            Destroy(col.gameObject);
            Destroy(transform.parent.gameObject,0.5f);//停留0.5s
        }
    }
}
