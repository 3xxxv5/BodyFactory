using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEmitManager : MonoBehaviour
{
    public static DragonEmitManager _instance { get; private set; }

    //ball 自机狙
    [Header("自机狙")]
    [Space(8)]
    public GameObject ballPrefab;
    public int ballAmount = 5;
    public int ballPerCount = 5;
    public float ballPerRate = 2f;
    public float ballWaveRate =3F;
    //circle ball 圆圈散弹
    [Header("圆圈散弹")]
    [Space(8)]
    public Transform dirCenter;
    public Transform[] dirs;
    public GameObject circlePrefab;
    public int circleAmount = 5;
    public float circlePerRate = 0;
    public float circleWaveRate = 3f;
    private void Awake()
    {
        _instance = this;
    }

    public IEnumerator SpawnLightningBall()
    {
        for (int v = 0; v < ballAmount; v++)
        {
            int index = Random.Range(1, 5);//1-4
            AudioManager._instance.PlayEffect("dragonBall" + index.ToString());//发出电球的声音-自机狙
            for (int i = 0; i < ballPerCount; i++)
            {
                Instantiate(ballPrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(ballPerRate);  //放完1个等2s
            }
            yield return new WaitForSeconds(ballWaveRate);//放完1轮等3s
        }
    }

    public IEnumerator  SpawnCircleBall()
    {
        for (int v = 0; v < circleAmount; v++)//放5次
        {
            int index = Random.Range(1,5);//1-4
            AudioManager._instance.PlayEffect("dragonBall"+index.ToString());//发出电球的声音-圆形轨迹
            for (int i = 0; i < dirs.Length; i++)//每次放12个
            {
                DragonCircleBall ball = Instantiate(circlePrefab, transform.position, Quaternion.identity).GetComponent<DragonCircleBall>();
                ball.target = dirs[i];
                ball.center = dirCenter;
                yield return new WaitForSeconds(circlePerRate);//放完1个等0s
            }
            yield return new WaitForSeconds(circleWaveRate);//放完1轮等3s
        }
       
    }
}
