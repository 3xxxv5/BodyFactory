using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBullet : MonoBehaviour
{
    bool hasPlay = false;
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("monster"))
        {
            if (!hasPlay)
            {
                AudioManager._instance.PlayEffect("gun");//炮台的炸弹攻击音效
                hasPlay = true;
            }          

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
            if (GameManager2._instance.levelNow == GameManager2.LevelNow.isLevel2)
            {
                GameManager2._instance.level2foodNum--;
            }
            //同归于尽
            Destroy(col.gameObject);
            Destroy(transform.parent.gameObject,0.5f);//停留0.5s
        }
    }
}
