using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedReflectFairy : ReflectFairy
{

    protected override void MoveFairy()
    {
        //不可移动
    }
    protected override void FollowTarget()
    {
        //不可跟随
    }
}
