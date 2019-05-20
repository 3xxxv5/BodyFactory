using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pearl : MonoBehaviour
{
    float produceTimer1=5;
    float ticktock1 = 0;
    float produceTimer2=10;
    float ticktock2 = 0;
    float produceTimer3=15;
    float ticktock3 = 0;
    [HideInInspector]public bool firstOk = true;
    [HideInInspector]public bool secondOk = true;
    [HideInInspector]public bool thirdOk = true;
    //public Transform[] bullets;
    //public Animator[] shellAnimators;

    void Update()
    {
        if (!firstOk)
        {
            ticktock1 += Time.deltaTime;
            float fillAmount = 1 - (ticktock1 / produceTimer1);
            Level2UIManager._instance.circleProgress[0].fillAmount = fillAmount;
            if (ticktock1 >= produceTimer1)
            {
                ticktock1 = 0;
                Level2UIManager._instance.circleProgress[0].fillAmount = 0;
                firstOk = true;
            }
        }
        if (!secondOk)
        {
            ticktock2 += Time.deltaTime;
            float fillAmount = 1 - (ticktock2 / produceTimer2);
            Level2UIManager._instance.circleProgress[1].fillAmount = fillAmount;
            if (ticktock2 >= produceTimer2)
            {
                ticktock2 = 0;
                Level2UIManager._instance.circleProgress[1].fillAmount = 0;
                secondOk = true;
            }
        }
        if (!thirdOk)
        {
            ticktock3 += Time.deltaTime;
            float fillAmount = 1 - (ticktock3 / produceTimer3);
            Level2UIManager._instance.circleProgress[2].fillAmount = fillAmount;
            if (ticktock3 >= produceTimer3)
            {
                ticktock3 = 0;
                Level2UIManager._instance.circleProgress[2].fillAmount = 0;
                thirdOk = true;
            }
        }
    }
}
