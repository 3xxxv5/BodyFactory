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
    private void Awake()
    {
        dragon = transform.parent.GetComponent<Dragon>();
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("wuzei"))
        {           
            dragon.CollideBlood(1,dragonBlood);
            Destroy(gameObject);//销毁碰撞体
        }
    }
}
