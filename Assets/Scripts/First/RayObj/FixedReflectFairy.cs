using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixedReflectFairy : ReflectFairy
{
    public override void InitReflectRay()
    {
        reflectRay = GetComponentInChildren<ReflectRayEmitter>();
        reflectRay.startPoint = transform.position;
        reflectRay.dirNum = dirNum;
        reflectRay.dirs = new Vector3[dirNum] //16个方向，记住序号，下次继续往下轮
        {
            new Vector3(-1,1,0),new Vector3(-1,1,1),
            new Vector3(0,1,1), new Vector3(1,1,1),
            new Vector3(1,1,-1), new Vector3(0,1,-1),
            new Vector3(-1,1,-1), new Vector3(1,1,0)            
        };
    }

    public override void InitButtons()
    {
        fairySorts = FairySorts.FixedReflect;
        if (rotateButton != null)
        {
            rotateButton.onClick.AddListener(RotateFairy);
        }
        if (overTurnButton != null)
        {
            overTurnButton.onClick.AddListener(OverTurnFairy);
        }
        //rotateButton = GameObject.FindWithTag("fixedRotateBtn").GetComponent<Button>();

        //overTurnButton = GameObject.FindWithTag("fixedTurnBtn").GetComponent<Button>();
    }

    public override void MoveFairy()
    {
        //不可移动
    }
    public override void FollowTarget()
    {
        //不可跟随
    }
}
