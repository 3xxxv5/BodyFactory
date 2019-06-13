using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DargonBloodState
{
    bloodHead,
    bloodTail,
    bloodChest,
}
public class DragonBlood : MonoBehaviour
{
    private Dragon dragon;
    public DargonBloodState dragonBlood;//设置自己的属性，被攻击后调整相应的mat参数
    bool canCollide = true;

    private void Awake()
    {
        dragon = transform.parent.GetComponent<Dragon>();
        print(dragon.gameObject.name);
    }
    private void OnTriggerEnter(Collider col)
    {
        if (!canCollide) return;
        if (col.gameObject.layer == LayerMask.NameToLayer("wuzei"))
        {
            print("掉血了");
            dragon.CollideBlood(1,dragonBlood,transform.position);
            Destroy(gameObject);//销毁碰撞体
        }
        canCollide = false;//碰撞一次之后先禁用1s
        StartCoroutine(waitToCollide(1));
    }
    IEnumerator waitToCollide(float time)
    {
        yield return new WaitForSeconds(time);
        canCollide = true;
    }
}
