using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables;
using DG.Tweening;
public class GameManager2 : MonoBehaviour
{
    #region 变量
    public static GameManager2 _instance { get; private set; }
    #region  level-settings
    [Header("Level 1")]
    [Space(8)]
    public Transform level1Sea;
    public GameObject level1Collider;
    public int level1HpBase = 5;
    [HideInInspector]    public int level1Hp=0;
    public PlayableDirector level1Clip;
    public Camera level1Camera;
    public int level1foodAmount=0;
    public int level1foodAmountBase=0;
    [Header("Level 2")]
    [Space(8)]
    public Transform level2Sea;
    public Transform level2Collider;
    public int level2HpBase = 8;
    [HideInInspector]    public int level2Hp=0;
    public PlayableDirector level2Clip;
    public Camera level2Camera;
    public int level2foodAmount=0;
    public int level2foodAmountBase=0;
    public Animator dragonRoadAnim;

    enum LevelNow
    {
        isLevel1,
        isLevel2,
        isLevel3
    }
    LevelNow levelNow = LevelNow.isLevel1;
    #endregion

    [Header("View")]
    [Space(8)]

    [HideInInspector]public  bool hasOver = false;

    //切换炮台
    public Transform wuzei;
    public BatteryAIO battery;

     public  bool canChange2Battery = true;
    public float boomDistance = 0.5f;

    //掉落物buff
    [HideInInspector] public float buffDropSpeed=0;
    float lowSpeedTime = 5f;

    //MonsterColor
    public Color once;
    public Color twice;
    public Color thirdTimes;
    public int dragonHp = 3;
    #endregion

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        Level1_Init();  
        Change2PlayerView();
    }
    public void SpawnSpecialEffects(string name,Vector3 pos,float stayTime)
    {
        Destroy(Instantiate(Resources.Load<GameObject>("Prefabs/"+name),pos,Quaternion.identity),stayTime);
    }

    void Update()
    {

        switch (levelNow)
        {
            case LevelNow.isLevel1:
                CheckLevel1Over();
                break;
            case LevelNow.isLevel2:
                CheckLevel2Over();
                break;
        }
    }

    #region 切换炮台-settings
    public void Change2BatteryView()
    {
        AudioManager._instance.PlayEffect("toBattery");//音效
        Level2UIManager._instance.simpleCross.gameObject.SetActive(false);
        Level2UIManager._instance.batteryCross.gameObject.SetActive(true);
        wuzei.gameObject.SetActive(false);
        battery.canShoot = true; battery.enableCameraMovement = true;
        canChange2Battery = false;

    }
    public void Change2PlayerView()
    {
        AudioManager._instance.PlayEffect("toBattery");//音效
        Level2UIManager._instance.simpleCross.gameObject.SetActive(true);
        Level2UIManager._instance.batteryCross.gameObject.SetActive(false);
        wuzei.gameObject.SetActive(true);
        battery.ResetBatteryPos();
        battery.canShoot = false;battery.enableCameraMovement = false;
    }
    #endregion

    #region 关卡节奏-settings
    void Level1_Init()
    {
        level1Hp = level1HpBase;
        levelNow = LevelNow.isLevel1;
        level1foodAmount = 0;
        Utility.DisableCamera(level1Camera.transform);
        //主角位置

        //楼层开启

        //Wave数组选用
    }
    void Level2_Init()
    {
        level2Hp = level2HpBase;
        EmitManager._instance.CountAmount(EmitManager._instance.SecondLevelWave,ref level2foodAmountBase);//计算总数

        level2Collider.GetComponent<Collider>().enabled = true;
        levelNow = LevelNow.isLevel2;
        StartCoroutine(EmitManager._instance.SpawnWave2(EmitManager._instance.SecondLevelWave, 2));
    }


    void CheckLevel1Over()
    {
        Level2UIManager._instance.lifeSlider.value = (float)level1Hp / (float)level1HpBase;
        Level2UIManager._instance.waveSlider.value = (float)level1foodAmount / (float)level1foodAmountBase;
        if (level1Hp <= 0)
        {
            Level1Over();
        }
    }
    void CheckLevel2Over()
    {
        Level2UIManager._instance.lifeSlider.value = (float)level2Hp / (float)level2HpBase;
        Level2UIManager._instance.waveSlider.value = (float)level2foodAmount / (float)level2foodAmountBase;
        if (level2Hp <= 0)
        {
            Level1Over();
        }
    }

    public void CheckWin(int level)//发射完最后一波后检测
    {
        switch (level)
        {
            case 1:
                CheckLevel1Win();
                break;
            case 2:
                CheckLevel2Win();
                break;
        }
    }
    void Level1Over()
    {
        if (!hasOver)
        {
            AudioManager._instance.PlayEffect("gameOver2");
            FirstPersonAIO._instance.GameOver();
            //出现UI 
            Utility.EnableCanvas(Level2UIManager._instance.overCanvas, 1f);
            hasOver = true;
        }
    }
    
    public  IEnumerator SeaDead(float toBlack,float toClear,float waitTime,Transform reviveTrans)
    {//并不是直接死，需要重新来过。而是短暂黑屏，首先玩家行动肯定受到了限制，然后其他物体先停止降落
        AudioManager._instance.PlayEffect("oops");
        FirstPersonAIO._instance.enableCameraMovement = false;
        Level2UIManager._instance.fadeImage.DOFade(1f,toBlack);
        yield return new WaitForSeconds(toBlack+waitTime);
        FirstPersonAIO._instance.transform.position = reviveTrans.position;//世界坐标
        FirstPersonAIO._instance.transform.rotation = reviveTrans.rotation;
        FirstPersonAIO._instance.transform.localScale = reviveTrans.localScale;
        Level2UIManager._instance.fadeImage.DOFade(0f,toClear);
        AudioManager._instance.PlayEffect("revise");//音效
        yield return new WaitForSeconds(toClear);       
        FirstPersonAIO._instance.enableCameraMovement = true;
    }

    public void ToNextLevel()
    {
        SceneManager.LoadScene("3_end_anim");
        Save._instance.SaveIkaCoinsAndTime(Level2UIManager._instance.ikaCoinsNum,Level2UIManager._instance.gameTime);
        Cursor.visible = true;Cursor.lockState = CursorLockMode.None;
    }

    public void CheckLevel1Win()
    {
        if (level1Hp >= 0)
        {
            AudioManager._instance.PlayEffect("gameWin");
            ToNextLevel();
            //过场动画
            //StartCoroutine(WinAnim(level1Clip));
        }
    }
    public void CheckLevel2Win()
    {
        if (level2Hp >= 0&&dragonHp<=0)
        {
            AudioManager._instance.PlayEffect("gameWin");
            //过场动画
            StartCoroutine(Level2Win());
        }
    }

    IEnumerator Level2Win()
    {
          yield return null;
          Level2UIManager._instance.fadeImage.DOFade(1, Level2UIManager._instance.toBlackTime);
          
    }
    IEnumerator WinAnim(PlayableDirector clip)
    {
        //变黑
        Level2UIManager._instance.fadeImage.color = Color.clear;
        Level2UIManager._instance.fadeImage.DOFade(1, Level2UIManager._instance.toBlackTime);
        Utility.DisableCanvas(Level2UIManager._instance.dataCanvas,1f);
       
        yield return new WaitForSeconds(Level2UIManager._instance.toBlackTime);
        Utility.DisableCamera(FirstPersonAIO._instance.transform);
        Utility.EnableCamera(level1Camera.transform);
        Utility.DisableCanvas(Level2UIManager._instance.crossCanvas,1f);

        //全黑时开始播过场
        clip.Play();
        //同时开始播变白的动画
        Level2UIManager._instance.fadeImage.DOFade(0, Level2UIManager._instance.toClearTime);
        yield return new WaitForSeconds(Level2UIManager._instance.toClearTime);
        //变白的动画播完时,还剩1.4s
        yield return new WaitForSeconds(((float)clip.duration- Level2UIManager._instance.toClearTime)*0.9f);
        Level2UIManager._instance.waveSlider.value = 0;
        Level2UIManager._instance.lifeSlider.value = 1;
        //播到还差20%结束时，开始变黑
        Level2UIManager._instance.fadeImage.DOFade(1, Level2UIManager._instance.toBlackTime);       
        yield return new WaitForSeconds(Level2UIManager._instance.toBlackTime);
        Destroy(level1Collider.gameObject); Destroy(level1Sea.gameObject);
        level2Sea.gameObject.SetActive(true);
        Utility.DisableCamera(level1Camera.transform);
        Utility.EnableCamera(FirstPersonAIO._instance.transform);

        Utility.EnableCanvas(Level2UIManager._instance.dataCanvas, Level2UIManager._instance.toClearTime);
        Utility.EnableCanvas(Level2UIManager._instance.crossCanvas, Level2UIManager._instance.toClearTime);
        Level2UIManager._instance.fadeImage.DOFade(0, Level2UIManager._instance.toClearTime);
        yield return new WaitForSeconds(Level2UIManager._instance.toClearTime);
        Level2_Init();
    }
    #endregion

    #region 掉落物buff-settings
    public IEnumerator ChangeAllDropSpeed()
    {
        buffDropSpeed -= 1f;
        yield return new WaitForSeconds(lowSpeedTime);

        buffDropSpeed = 0;
    }
    #endregion

    #region UI-Settings
    public void ButtonDown(string name)
    {
        switch (name)
        {
            case "restart":
                Level1Restart();
                break;
            case "lastLevel":
                ToLastLevel();
                break;
        }
    }
    void Level1Restart()
    {
        FirstPersonAIO._instance.enableCameraMovement = true;
        FirstPersonAIO._instance.gameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void ToLastLevel()
    {
        SceneManager.LoadScene("1_official");
    }
    #endregion

}
