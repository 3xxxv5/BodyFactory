using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 _instance { get; private set; }

    [Header("Level 1")]
    [Space(8)]
    public Transform level1Sea;
    public GameObject level1SeaCollider;
    public GameObject level1WallCollider;
    public int level1HpBase = 5;
    [HideInInspector]    public int level1Hp=0;
    public int level1foodNum=0;
    public int level1foodBase=0;
    [Header("Level 2")]
    [Space(8)]
    public Transform level2Sea;
    public GameObject level2SeaCollider;
    public GameObject level2WallCollider;
    public GameObject level2DoorCollider;
    public GameObject level3SeaCollider;
    public GameObject level3IslandCollider;
    public int level2HpBase = 8;
    [HideInInspector]    public int level2Hp=0;
    public int level2foodNum=0;
    public int level2foodBase=0;
    [Header("Level 3")]
    [Space(8)]
    public GameObject level3WallCollider;

    bool hasWin1 = false;
    bool hasWin2= false;
    bool hasWin3 = false;
    [HideInInspector] public bool hasOver1 = false;
    [HideInInspector] public bool hasOver2 = false;
    [HideInInspector] public bool hasOver3 = false;
    public  enum LevelNow
    {
        isLevel1,
        isLevel2,
        isLevel3
    }
    [HideInInspector]public  LevelNow levelNow = LevelNow.isLevel1;

    private void Awake()
    {
        _instance = this;
        Level1_Init();
    }
    void Level1_Init()
    {
        level1Hp = level1HpBase;
        levelNow = LevelNow.isLevel1;
        level1foodNum = 0;
        hasWin1 = false;
        //水面碰撞盒，只能禁用collider，不能禁用该物体，水面是透明的，需要物体在下面衬着
        level2SeaCollider.GetComponent<Collider>().enabled = false;
        level3SeaCollider.GetComponent<Collider>().enabled = false;
        //墙的碰撞盒
        level1WallCollider.SetActive(true);
        level2WallCollider.SetActive(false);
        level2DoorCollider.SetActive(false);
        level3WallCollider.SetActive(false);
        level3IslandCollider.SetActive(false);
    }
    public  void Level2_Init()
    {
        level2Hp = level2HpBase;
        EmitManager._instance.CountAmount(EmitManager._instance.SecondLevelWave, ref level2foodBase);//计算总数
        level2SeaCollider.GetComponent<Collider>().enabled = true;    
        levelNow = LevelNow.isLevel2;
        //墙的碰撞盒
        level2WallCollider.SetActive(true);
        level2DoorCollider.SetActive(true);
        level3WallCollider.SetActive(false);

        StartCoroutine(EmitManager._instance.SpawnWave(EmitManager._instance.SecondLevelWave));
    }
    public void Level3_Init()
    {
        levelNow = LevelNow.isLevel3;
        level3SeaCollider.GetComponent<Collider>().enabled = true;
        //墙的碰撞盒
        level3WallCollider.SetActive(true);
        level3IslandCollider.SetActive(true);

        StartCoroutine(DragonEmitManager._instance.SpawnLightningBall());
        AudioManager._instance.PlayeBGM("battle");//播放boss关bgm
    }
    void Update()
    {
        switch (levelNow)
        {
            case LevelNow.isLevel1:
                CheckLevel1Over();
                CheckLevel1Win();
                break;
            case LevelNow.isLevel2:
                CheckLevel2Over();
                CheckLevel2Win();
                break;
            case LevelNow.isLevel3:
                CheckLevel3Over();
                CheckLevel3Win();
                break;
        }
    } 

    void CheckLevel1Over()
    {
        Level2UIManager._instance.lifeSlider.value =(float)level1Hp / (float)level1HpBase;
        Level2UIManager._instance.waveSlider.value = (float)level1foodNum / (float)level1foodBase;
        if (level1Hp <= 0)
        {
            if (!hasOver1)
            {
                AudioManager._instance.PlayEffect("gameOver2");
                FirstPersonAIO._instance.GameOver();
                //出现UI 
                Utility.EnableCanvas(Level2UIManager._instance.overCanvas, 1f);
                hasOver1 = true;
            }
        }
    }
    void CheckLevel1Win()
    {
        if (!hasWin1)
        {
            if (EmitManager._instance.hasAllSpawned && level1Hp >= 0 && level1foodNum <= 0)
            {
                AudioManager._instance.PlayEffect("gameWin");
                StartCoroutine(TimelineManager2._instance.Level1WinAnim(TimelineManager2._instance.level1Clip));   //过场动画
                hasWin1 = true;
            }
        }
    }
    void CheckLevel2Over()
    {
        Level2UIManager._instance.lifeSlider.value = (float)level2Hp / (float )level2HpBase;
        Level2UIManager._instance.waveSlider.value = (float)level2foodNum / (float)level2foodBase;
        if (level2Hp <= 0)
        {
            if (!hasOver2)
            {
                AudioManager._instance.PlayEffect("gameOver2");
                FirstPersonAIO._instance.GameOver();  
                Utility.EnableCanvas(Level2UIManager._instance.overCanvas, 1f);  //出现UI 
                hasOver2 = true;
            }
        }
    }
    void CheckLevel2Win()
    {
        if (!hasWin2)
        {
            if (EmitManager._instance.hasAllSpawned && level2Hp >= 0 && level2foodNum <= 0)
            {
                AudioManager._instance.PlayEffect("gameWin");
                StartCoroutine(TimelineManager2._instance.Level2WinAnim()); //过场动画
                hasWin2 = true;
            }
        }
    }
    void CheckLevel3Over()
    {

    }
    void CheckLevel3Win()
    {
        if (DragonManager._instance.dragon.hasDead)
        {
            if (!hasWin3)
            {
                print("赢了");
                StartCoroutine(TimelineManager2._instance.level3WinAnim()); //过场动画
                hasWin3 = true;
            }            
        }
    }
    
    public  IEnumerator SeaDead(float toBlack,float toClear,float waitTime,Transform reviveTrans)
    {
        //并不是直接死，需要重新来过。而是短暂黑屏，首先玩家行动肯定受到了限制，然后其他物体先停止降落
        AudioManager._instance.PlayEffect("oops");
        FirstPersonAIO._instance.enableCameraMovement = false;
        Level2UIManager._instance.fadeImage.DOFade(1f,toBlack);
        //Collider[] wuzeiColliders=  FirstPersonAIO._instance.transform.GetComponentsInChildren<Collider>();
        //for(int i = 0; i < wuzeiColliders.Length; i++)
        //{
        //    wuzeiColliders[i].enabled = false;
        //}
        yield return new WaitForSeconds(toBlack+waitTime);

        FirstPersonAIO._instance.transform.position = reviveTrans.position;//世界坐标
        FirstPersonAIO._instance.transform.rotation = reviveTrans.rotation;
        FirstPersonAIO._instance.transform.localScale = reviveTrans.localScale;

        Level2UIManager._instance.fadeImage.DOFade(0f,toClear);
        AudioManager._instance.PlayEffect("revise");//音效

        yield return new WaitForSeconds(toClear);
        //for (int i = 0; i < wuzeiColliders.Length; i++)
        //{
        //    wuzeiColliders[i].enabled = true;
        //}
        FirstPersonAIO._instance.enableCameraMovement = true;
    }

    public void ToNextLevel()
    {
        SceneManager.LoadScene("3_end_anim");
        Save._instance.SaveIkaCoinsAndTime(Level2UIManager._instance.ikaCoinsNum,Level2UIManager._instance.gameTime);
        Cursor.visible = true;Cursor.lockState = CursorLockMode.None;
    }
    
   public  void LevelRestart()
    {
        FirstPersonAIO._instance.enableCameraMovement = true;
        FirstPersonAIO._instance.gameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//通通重新来过
    }

    public void ChangeSea()
    {
        Destroy(level1SeaCollider);
        Destroy(level1Sea.gameObject);
        level2Sea.gameObject.SetActive(true);
    }
}
