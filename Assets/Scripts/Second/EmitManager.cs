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
    [HideInInspector]public bool hasAllSpawned = false;

    private void Awake()
    {
        _instance = this;

        //获取第一关全部的食物数量，用于UI的wave slider显示
        CountAmount(FirstLevelWave, ref GameManager2._instance.level1foodBase);

        //开始掉落第一关的食物
        StartCoroutine(SpawnWave(FirstLevelWave));

        //设置光圈的初始scale为0，隐藏
        lightCircleAnim.transform.localScale = Vector3.zero;
        dragonCircleAnim.transform.localScale = Vector3.zero;
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

    public IEnumerator SpawnWave(MyQueue[] levelWave )
    {
        hasAllSpawned = false;
        for (int v = 0; v < levelWave.Length;v++ )
        {         
            for (int i=0; i < levelWave[v].count;i++ )//小队里怪物的个数
            {
                SpawnFood(levelWave[v].foodPrefab);//直接生成第一个。生成最后一个后还要等10s吗？是的
                switch (GameManager2._instance.levelNow)
                {
                    case GameManager2.LevelNow.isLevel1:
                        GameManager2._instance.level1foodNum++;
                        GameManager2._instance.level1foodShow++;
                        break;
                    case GameManager2.LevelNow.isLevel2:
                        GameManager2._instance.level2foodNum++;
                        GameManager2._instance.level2foodShow++;
                        break;
                }
                yield return new WaitForSeconds(levelWave[v].rate);//等10s     
            }
            if (v != levelWave.Length - 1)
            {
                yield return new WaitForSeconds(waveRate);
            }
        }
        hasAllSpawned = true;
    } 

    void SpawnFood(GameObject prefab)
    {
        if (!FirstPersonAIO._instance.gameOver)
        {
            int random_x = Random.Range(-randomRange, randomRange);
            int random_z = Random.Range(-randomRange, randomRange);
            Vector3 spawnPos = transform.position + new Vector3(random_x, 0, random_z);
            switch (prefab.tag)
            {
                case "chocolateFrog":
                    AudioManager._instance.PlayEffect("frog");
                    ShowLightCircle(prefab, spawnPos);
                    break;
                case "dragon":
                    AudioManager._instance.PlayEffect("skr");
                    FirstPersonAIO._instance.attackDragon = true;
                    dragonCircleAnim.SetTrigger("showCircle");
                    StartCoroutine(WaitSpawn(prefab, spawnPos, 0f));
                    break;
                default:
                    AudioManager._instance.PlayEffect("foodDrop");
                    ShowLightCircle(prefab,spawnPos);
                    break;
            }
        }
    }

    void ShowLightCircle(GameObject prefab, Vector3 spawnPos)
    {
        lightCircleAnim.transform.position = spawnPos + Vector3.up;
        lightCircleAnim.SetTrigger("showCircle");
        print("应该是执行了这里");
        StartCoroutine(WaitSpawn(prefab, spawnPos, 0.5f));
    }

    IEnumerator WaitSpawn(GameObject prefab,Vector3 spawnPos,float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(prefab, spawnPos, prefab.transform.rotation);
    }

}

