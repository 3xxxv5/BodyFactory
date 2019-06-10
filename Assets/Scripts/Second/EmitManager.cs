using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EmitManager : MonoBehaviour
{
    public static EmitManager _instance { get; private set; }
    public Animator lightCircleAnim;
    public Animator dragonCircleAnim;
    public int randomRange = 18;
    public float waveRate = 5;
    public MyQueue[] FirstLevelWave;
    public MyQueue[] SecondLevelWave;

    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        Init();
        StartCoroutine(SpawnWave1(FirstLevelWave, 1));
        lightCircleAnim.transform.localScale = Vector3.zero;
        dragonCircleAnim.transform.localScale = Vector3.zero;
    }
    void Init()
    {
        //获取第一关全部的食物数量
        CountAmount(FirstLevelWave, ref GameManager2._instance.level1foodAmountBase);
    }
    public void CountAmount(MyQueue[] levelWave, ref int amount)
    {
        for (int v = 0; v < levelWave.Length; v++)  {

                for (int i = 0; i < levelWave[v].count; i++)//小队里怪物的个数
                {
                    amount++;
                }
        }
    }

    public IEnumerator SpawnWave1(MyQueue[] levelWave, int level)
    {
        for (int v = 0; v < levelWave.Length; v++)
        {         
            for (int i = 0; i < levelWave[v].count; i++)//小队里怪物的个数
            {
                SpawnOne(levelWave[v].foodPrefab);//直接生成第一个。生成第3个后还要等10s吗？是的
                yield return new WaitForSeconds(levelWave[v].rate);//等10s           
            }
            if (v != levelWave.Length - 1)
            {
                yield return new WaitForSeconds(waveRate);//每一波之间要等的时间
            }
        }
        //全部发射完后进行检测
        GameManager2._instance.CheckWin(level);
    }
    public IEnumerator SpawnWave2(MyQueue[] levelWave, int level)
    {
        for (int v = 0; v < levelWave.Length; v++)
        {
            for (int i = 0; i < levelWave[v].count; i++)//小队里怪物的个数
            {
                SpawnTwo(levelWave[v].foodPrefab);//直接生成第一个。生成第3个后还要等10s吗？是的
                yield return new WaitForSeconds(levelWave[v].rate);//等10s           
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
            if (prefab.tag.Equals("chocolateFrog"))
            {
                AudioManager._instance.PlayEffect("frog");
            }
            else
            {
                AudioManager._instance.PlayEffect("foodDrop");
            }
            int random_x = Random.Range(-randomRange, randomRange);
            int random_z = Random.Range(-randomRange, randomRange);
            GameManager2._instance.level1foodAmount++;
            Vector3 spawnPos = transform.position + new Vector3(random_x, 0, random_z);
            if (prefab.tag.Equals("dragon"))
            {              
                dragonCircleAnim.SetTrigger("showCircle");
                StartCoroutine(WaitSpawn(prefab, spawnPos, 0f));
            }
            else
            {
                lightCircleAnim.transform.position = spawnPos + Vector3.up;
                lightCircleAnim.SetTrigger("showCircle");
                StartCoroutine(WaitSpawn(prefab, spawnPos, 0.5f));
            }   

        }
    }
    void SpawnTwo(GameObject prefab)
    {
        if (!FirstPersonAIO._instance.gameOver)
        { 
            GameManager2._instance.level2foodAmount++;
            if (prefab.tag.Equals("chocolateFrog"))
            {
                AudioManager._instance.PlayEffect("frog");
            }
            else if (prefab.tag.Equals("dragonRoad"))
            {
                AudioManager._instance.PlayEffect("skr");
                FirstPersonAIO._instance.attackDragon = true;
            }
            else
            {
                AudioManager._instance.PlayEffect("foodDrop");
            }

            int random_x = Random.Range(-randomRange, randomRange);
            int random_z = Random.Range(-randomRange, randomRange);
            Vector3 spawnPos = transform.position + new Vector3(random_x, 0, random_z);
            lightCircleAnim.transform.position = spawnPos + Vector3.up;
            lightCircleAnim.SetTrigger("showCircle");
            StartCoroutine(WaitSpawn(prefab, spawnPos,0.5f));         

        }
    }

    IEnumerator WaitSpawn(GameObject prefab,Vector3 spawnPos,float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(prefab, spawnPos, prefab.transform.rotation);
    }
}

