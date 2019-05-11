using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pearl : MonoBehaviour
{
    float ticktock = 0;
    public  Transform[] bullets;

    private void Awake()
    {
        BatteryAIO._instance.shotCount = BatteryAIO._instance.shotMaxAmount;
    }
    // Update is called once per frame
    void Update()
    {
        //SetShellBullets();

        ticktock += Time.deltaTime;
        if (ticktock >= BatteryAIO._instance.produceTimer)
        {
            ticktock = 0;
            if (BatteryAIO._instance.shotCount < BatteryAIO._instance.shotMaxAmount)
            {
                BatteryAIO._instance.shotCount++;
            }
        }
    }

    void SetShellBullets()
    {
        for (int i = 0; i < BatteryAIO._instance.shotCount; i++)
        {
            bullets[i].gameObject.SetActive(true);
        }
        for (int i = BatteryAIO._instance.shotMaxAmount - 1; i >= BatteryAIO._instance.shotCount; i--)
        {
            bullets[i].gameObject.SetActive(false);
        }
    }
}
