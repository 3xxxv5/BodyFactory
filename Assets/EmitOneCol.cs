using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitOneCol : MonoBehaviour
{
    Collision lastCol=null;

    private void OnCollisionEnter(Collision col)
    {    
        if (col != lastCol)//保证此函数只执行一次
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("monster"))
            {
                GameManager2.level1Hp -= 1;    //碰到障碍物，生命值就减1
                lastCol = col;
            }
        }
    }
}
