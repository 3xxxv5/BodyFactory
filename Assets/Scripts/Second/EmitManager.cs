using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitManager : MonoBehaviour {
    public static EmitManager _instance { get; private set; }
    public  float timer = 4;
    float time = 0;
    public int randomRange =18;
    int randomMonster = 0;
    public float queueRate = 0;
    public float waveRate = 5;
    public BigWave[] FirstLevelWave;
    public BigWave[] SecondLevelWave;
    GameObject donut;
    GameObject lollipop;
    GameObject macaron;

    private void Awake()
    {
        _instance = this;
    }
    void Start () {
        InitTest();
        StartCoroutine(SpawnWave1(FirstLevelWave,1));
    }
    void InitTest()
    {
        CountAmount(FirstLevelWave, ref GameManager2._instance.level1foodAmountBase);
    }
   public void CountAmount(BigWave[] levelWave, ref int amount)
    {
        for (int v = 0; v < levelWave.Length; v++)
        {
            for (int j = 0; j < levelWave[v].myQueues.Length; j++)//第一波里的3个小队
            {
                for (int i = 0; i < levelWave[v].myQueues[j].count; i++)//小队里怪物的个数
                {
                    amount++;                   
                }
            }
        }
    }
    void Update () {
        
    }
   public  IEnumerator SpawnWave1(BigWave[] levelWave,int level)
    {
        for(int v = 0; v < levelWave.Length;v++)
        {
            for (int j = 0; j < levelWave[v].myQueues.Length; j++)//第一波里的3个小队
            {
                for (int i = 0; i < levelWave[v].myQueues[j].count; i++)//小队里怪物的个数
                {
                    SpawnOne(levelWave[v].myQueues[j].foodPrefab);//直接生成第一个。生成第3个后还要等10s吗？是的
                    yield return new WaitForSeconds(levelWave[v].myQueues[j].rate);//等10s           
                }
                yield return new WaitForSeconds(queueRate);//每一小队之间要等的时间。没有额外等的时间了   
            }
            if (v != levelWave.Length - 1)
            {
                yield return new WaitForSeconds(waveRate);//每一波之间要等的时间
            }            
        }
        //全部发射完后进行检测
        GameManager2._instance.CheckWin(level);
    }
    public IEnumerator SpawnWave2(BigWave[] levelWave, int level)
    {
        for (int v = 0; v < levelWave.Length; v++)
        {
            for (int j = 0; j < levelWave[v].myQueues.Length; j++)//第一波里的3个小队
            {
                for (int i = 0; i < levelWave[v].myQueues[j].count; i++)//小队里怪物的个数
                {
                    SpawnTwo(levelWave[v].myQueues[j].foodPrefab);//直接生成第一个。生成第3个后还要等10s吗？是的
                    yield return new WaitForSeconds(levelWave[v].myQueues[j].rate);//等10s           
                }
                yield return new WaitForSeconds(queueRate);//每一小队之间要等的时间。没有额外等的时间了   
            }
            if (v != levelWave.Length - 1)
            {
                yield return new WaitForSeconds(waveRate);//每一波之间要等的时间
            }
        }
        //全部发射完后进行检测
        GameManager2._instance.CheckWin(level);
    }

    void SpawnOne(GameObject prefab)
    {
        if (!FirstPersonAIO._instance.gameOver)
        {
            int random_x = Random.Range(-randomRange, randomRange);
            int random_z = Random.Range(-randomRange, randomRange);
            if (prefab.tag.Equals("chocolateFrog"))
            {
                AudioManager._instance.PlayEffect("frog");
            }
            else
            {
                AudioManager._instance.PlayEffect("refract");
            }
            Instantiate(prefab, transform.position + new Vector3(random_x, 0, random_z), prefab.transform.rotation);
            GameManager2._instance.level1foodAmount++;

        }
    }
    void SpawnTwo(GameObject prefab)
    {
        if (!FirstPersonAIO._instance.gameOver)
        {
            int random_x = Random.Range(-randomRange, randomRange);
            int random_z = Random.Range(-randomRange, randomRange);
            Instantiate(prefab, transform.position + new Vector3(random_x, 0, random_z), prefab.transform.rotation);
            GameManager2._instance.level2foodAmount++;
            AudioManager._instance.PlayEffect("refract");
        }
    }
}
