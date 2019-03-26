using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[System.Serializable]
public class SmallWave
{
    //public GameObject foodPrefab;
    public int count = 0;
    public float rate;
}
//[System.Serializable]
public class BigWave
{
    public List<SmallWave> smallWaves;
    public  BigWave()//List初始化
    {
        smallWaves = new List<SmallWave>(1);
        smallWaves[0].count = 0;
        smallWaves[0].rate = 1;
    }
}
