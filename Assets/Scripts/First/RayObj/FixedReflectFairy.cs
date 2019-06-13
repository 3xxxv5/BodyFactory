using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixedReflectFairy : ReflectFairy
{
    public override void InitButtons()
    {
        fairySorts = FairySorts.FixedReflect;
        rotateButton = GameObject.FindWithTag("fixedRotateBtn").GetComponent<Button>();
        rotateButton.onClick.AddListener(RotateFairy);
        overTurnButton = GameObject.FindWithTag("fixedTurnBtn").GetComponent<Button>();
        overTurnButton.onClick.AddListener(OverTurnFairy);
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
