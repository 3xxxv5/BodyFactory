using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitManager : MonoBehaviour {
    public  float timer = 4;
    float time = 0;
    public int randomRange =18;
    int randomMonster = 0;
    public float queueRate = 0;
    public float waveRate = 5;
    public BigWave[] FirstLevelWave;
    public BigWave[] SecondLevelWave;
    public BigWave[] ThirdLevelWave;
    GameObject donut;
    GameObject lollipop;
    GameObject macaron;

    void Start () {
        InitTest();
        StartCoroutine(SpawnWave(FirstLevelWave,1));
    }
    void InitTest()
    {
        donut = Resources.Load<GameObject>("Prefabs/m_donut");
        lollipop = Resources.Load<GameObject>("Prefabs/m_lollipop");
        macaron = Resources.Load<GameObject>("Prefabs/m_macaron");

        //FirstLevelWave = new BigWave();//5波  new这个对象时，调用了构函数  new对象数组
        //print(FirstLevelWave.smallWaves.Count);
        //SmallWave temp=new SmallWave();
        ////第一波：3个甜甜圈
        //temp.foodPrefab = donut;
        //temp.count = 3;
        //temp.rate = 10;
        #region 数组其他赋值
        //第二波：3个棒棒糖
        //temp.foodPrefab = lollipop;
        //temp.count = 3;
        //temp.rate = 7;
        //FirstLevelWave[1].smallWaves.Add(temp);
        ////第三波：5个马卡龙
        ////temp.foodPrefab = macaron;
        //temp.count = 5;
        //temp.rate = 5;
        //FirstLevelWave[2].smallWaves.Add(temp);
        ////第四波：3个甜甜圈
        ////temp.foodPrefab = donut;
        //temp.count = 3;
        //temp.rate = 10;
        //FirstLevelWave[3].smallWaves.Add(temp);
        ////第五波：3个甜甜圈、3个棒棒糖、3个马卡龙
        ////temp.foodPrefab = donut;
        //temp.count = 3;
        //temp.rate = 10;
        //FirstLevelWave[4].smallWaves.Add(temp);
        ////temp.foodPrefab = lollipop;
        //temp.count = 3;
        //temp.rate = 7;
        //FirstLevelWave[4].smallWaves.Add(temp);
        ////temp.foodPrefab = macaron;
        //temp.count = 3;
        //temp.rate = 5;
        //FirstLevelWave[4].smallWaves.Add(temp);
        #endregion
    }

    void Update () {
        
    }
   public  IEnumerator SpawnWave(BigWave[] levelWave,int level)
    {
        for(int v = 0; v < levelWave.Length;v++)
        {
            for (int j = 0; j < levelWave[v].myQueues.Length; j++)//第一波里的3个小队
            {
                for (int i = 0; i < levelWave[v].myQueues[j].count; i++)//小队里怪物的个数
                {
                    if (i == 1)
                    {
                        //全部发射完后进行检测
                        GameManager2._instance.CheckWin(level);
                    }
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
        
    }

  
    void SpawnOne(GameObject prefab)
    {
        if (!FirstPersonAIO._instance.enableCameraMovement) return;
        int random_x = Random.Range(-randomRange, randomRange);
        int random_z = Random.Range(-randomRange, randomRange);
        Instantiate(prefab, transform.position + new Vector3(random_x, 0, random_z), transform.rotation);
    }
}
