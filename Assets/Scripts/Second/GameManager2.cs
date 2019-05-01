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
    public GameObject level1Collider;
    public int level1HpBase = 5;
    [HideInInspector]
    public int level1Hp=0;
    public PlayableDirector level1Clip;
    public Camera level1Camera;
    public int level1foodAmount=0;
    public int level1foodAmountBase=0;
    [Header("Level 2")]
    [Space(8)]
    public GameObject level2Collider;
    public int level2HpBase = 8;
    [HideInInspector]
    public int level2Hp=0;
    public PlayableDirector level2Clip;
    public Camera level2Camera;
    public int level2foodAmount=0;
    public int level2foodAmountBase=0;
    [Header("Level 3")]
    [Space(8)]
    public GameObject level3Collider;
    public int level3HpBase = 10;
    [HideInInspector]
    public int level3Hp=0;
    public PlayableDirector level3Clip;
    public Camera level3Camera;
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
    public CanvasGroup overCanvas;
    public CanvasGroup playCanvas;
    public CanvasGroup crossCanvas;
    //切换炮台
    public Transform wuzei;
    public Transform paotong;
    Image simpleCross;
    Image batteryCross;
    private Slider waveSlider;
    private Slider lifeSlider;
    [HideInInspector]
    public  Animator lifeAnim;
    public Image blackPanel;
    bool hasOver = false;
    public float toBlackTime = 1f;
    public float toClearTime = 1f;
    [HideInInspector] public  bool canChange2Battery = true;
    [HideInInspector] public  GameObject fire;
    //音效
    [HideInInspector]public  AudioClip gunClip;
    public  float Volume = 5f;
    //掉落物buff
    [HideInInspector] public float buffDropSpeed=0;
    float lowSpeedTime = 5f;

    #endregion

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        //进度条 Panel
        waveSlider = playCanvas.transform.Find("WaveSlider").GetComponent<Slider>();
        lifeSlider = playCanvas.transform.Find("LifeSlider").GetComponent<Slider>();
        lifeAnim = lifeSlider.transform.Find("Handle Slide Area/Handle").GetComponent<Animator>();
        Utility.DisableCanvas(overCanvas,0.1f);
        //cross Panel
        simpleCross = crossCanvas.transform.Find("simpleCross").GetComponent<Image>();
        batteryCross = crossCanvas.transform.Find("batteryCross").GetComponent<Image>();
        batteryCross.gameObject.SetActive(false);
        Utility.DisableCanvas(crossCanvas, 0f);
        Utility.EnableCanvas(crossCanvas, 1f);
        //特效
        fire = Resources.Load<GameObject>("Prefabs/fire");
        //音效
        gunClip = Resources.Load<AudioClip>("Sound/Effect/gun");
        //Level
        Level1_Init();
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
        simpleCross.gameObject.SetActive(false);
        batteryCross.gameObject.SetActive(true);
        wuzei.gameObject.SetActive(false);
        paotong.gameObject.SetActive(true);
        canChange2Battery = false;
    }
    public void Change2PlayerView()
    {
        simpleCross.gameObject.SetActive(true);
        batteryCross.gameObject.SetActive(false);
        wuzei.gameObject.SetActive(true);
        paotong.gameObject.SetActive(false);
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
        lifeSlider.value = (float)level1Hp / (float)level1HpBase;
        waveSlider.value = (float)level1foodAmount / (float)level1foodAmountBase;
        if (level1Hp <= 0)
        {
            Level1Over();
        }
    }
    void CheckLevel2Over()
    {
        lifeSlider.value = (float)level2Hp / (float)level2HpBase;
        waveSlider.value = (float)level2foodAmount / (float)level2foodAmountBase;
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
            case 3:
                CheckLevel3Win();
                break;
        }
    }
    void Level1Over()
    {
        if (!hasOver)
        {
            AudioManager._instance.PlayEffect("oops");
            FirstPersonAIO._instance.GameOver();
            //出现UI 
            Utility.EnableCanvas(overCanvas, 1f);
            hasOver = true;
        }
    }
    public void CheckLevel1Win()
    {
        if (level1Hp >= 0)
        {
            AudioManager._instance.PlayEffect("tower");
            //过场动画
           // StartCoroutine(WinAnim(level1Clip));
        }
    }
    public void CheckLevel2Win()
    {
        if (level2Hp >= 0)
        {
            AudioManager._instance.PlayEffect("tower");
            //过场动画
            StartCoroutine(WinAnim(level2Clip));
        }
    }
    public void CheckLevel3Win()
    {
        if (level3Hp >= 0)
        {
            AudioManager._instance.PlayEffect("tower");
            //过场动画
            StartCoroutine(WinAnim(level3Clip));
        }
    }

    IEnumerator WinAnim(PlayableDirector clip)
    {
        //变黑
        blackPanel.DOFade(1, toBlackTime);
        Utility.DisableCanvas(playCanvas,1f);
        level1Collider.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(toBlackTime);
        Utility.DisableCamera(FirstPersonAIO._instance.transform);
        Utility.EnableCamera(level1Camera.transform);
        
        //全黑时开始播过场
        clip.Play();
        //同时开始播变白的动画
        blackPanel.DOFade(0, toClearTime);
        yield return new WaitForSeconds(toClearTime);
        //变白的动画播完时,还剩1.4s
        yield return new WaitForSeconds(((float)clip.duration-toClearTime)*0.9f);
        waveSlider.value = 0;
        lifeSlider.value = 1;
        //播到还差20%结束时，开始变黑
        blackPanel.DOFade(1, toBlackTime);
        yield return new WaitForSeconds(toBlackTime);

        Utility.DisableCamera(level1Camera.transform);
        Utility.EnableCamera(FirstPersonAIO._instance.transform);

        Utility.EnableCanvas(playCanvas, toClearTime);
        blackPanel.DOFade(0, toClearTime);
        yield return new WaitForSeconds(toClearTime);
        Level2_Init();

    }
    #endregion

    #region 掉落物buff-settings
    public IEnumerator ChangeAllDropSpeed()
    {
        buffDropSpeed -= 2.5f; 
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
