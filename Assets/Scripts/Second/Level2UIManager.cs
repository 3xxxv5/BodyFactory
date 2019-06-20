using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using DG.Tweening;
using taecg.tools;

public class Level2UIManager : MonoBehaviour
{
    public static Level2UIManager _instance { get; private set; }
    //data panel
    [HideInInspector] public CanvasGroup dataCanvas;
    public Slider waveSlider;
    public Slider lifeSlider;
    public Image[] circleProgress;
    public Text coinText;
    //cross panel
    [HideInInspector] public CanvasGroup crossCanvas;
    [HideInInspector] public  Image simpleCross;
    [HideInInspector] public Image batteryCross;
    //over panel
    [HideInInspector] public CanvasGroup overCanvas;
    [HideInInspector]public Image fadeImage;
    public Image whiteImage;
    [HideInInspector]public float toBlackTime = 1f;
    [HideInInspector] public float toClearTime = 1f;
    //pause panel
    [HideInInspector] public CanvasGroup pauseCanvas;
    public Text coin_fairyAmountText;
    public Text coin_ikaAmountText;
    public Text gameTimeText;
    public Transform coins;
    //转场动画
    public CircleWipe circleWipe;
    public CircleWipe circleWipeOutMask;
    CanvasGroup mainCanvas;
    [HideInInspector]public  bool canWipeIn = false;
    [HideInInspector]public bool canWipeOut = false;//当赢了要进入下一关时，设置为true
    float endValue = 0.55f;
    private float wipeOutSpeed = 0.5f;
    private float wipeInSpeed = 0.5f;

    [HideInInspector] public int ikaCoinsNum;
    [HideInInspector] public float gameTime;
    public GameObject tipText;

    private void Awake()
    {
        _instance = this;
        Init();
    }
    // Start is called before the first frame update
    void Start()
    {
        fadeImage = transform.Find("fadeImage").GetComponent<Image>();
        //fadeimage.color = color.black;
        //fadeimage.dofade(0f, 1f);
        print("coinIka的总数：" + (coins.childCount).ToString());
        tipText.SetActive(true);
    }
    void WipeIn()
    {
        if (canWipeIn)
        {
            circleWipe.Value += Time.deltaTime * wipeInSpeed;
            if (circleWipe.Value > endValue)
            {
                canWipeIn = false;
                circleWipe.Value = endValue;
                mainCanvas.alpha = 1;
            }
        }
    }
    void WipeOut()
    {
        if (canWipeOut)
        {
            circleWipeOutMask.Value -= Time.deltaTime * wipeOutSpeed;//从1-0，大约需要1s
            if (circleWipeOutMask.Value < 0.01f)
            {
                canWipeOut = false;
                circleWipeOutMask.Value = 0;
            }
        }
    }

    void Init()
    {
        AudioManager._instance.PlayeBGM("second");
        Save._instance.DeleteIkaCoinsAndTime();
        for (int i = 0; i < circleProgress.Length; i++) circleProgress[i].fillAmount = 0;
        //pause panel
        pauseCanvas = transform.Find("PausePanel").GetComponent<CanvasGroup>();
        Utility.DisableCanvas(pauseCanvas,0f);

        //data panel
        dataCanvas = transform.Find("DataPanel").GetComponent<CanvasGroup>();         
      
        //over panel
        overCanvas = transform.Find("GameOverPanel").GetComponent<CanvasGroup>();
        Utility.DisableCanvas(overCanvas, 0f);
        //转场：
        mainCanvas = transform.GetComponent<CanvasGroup>();
        mainCanvas.alpha = 0;
        circleWipe.Value = 0;
        circleWipeOutMask.Value = 1;
        canWipeIn = true;

        //cross Panel
        crossCanvas = transform.Find("CrossPanel").GetComponent<CanvasGroup>();
        simpleCross = crossCanvas.transform.Find("simpleCross").GetComponent<Image>();
        batteryCross = crossCanvas.transform.Find("batteryCross").GetComponent<Image>();
        batteryCross.gameObject.SetActive(false);
        Utility.DisableCanvas(crossCanvas, 0f);
        Utility.EnableCanvas(crossCanvas, 1f);

    }

    // Update is called once per frame
    void showCoins()
    {
        coin_ikaAmountText.text = ikaCoinsNum.ToString() + MainContainer.ikaAll;
        if (PlayerPrefs.HasKey(MainContainer.fairyCoins))
        {
            coin_fairyAmountText.text = PlayerPrefs.GetInt(MainContainer.fairyCoins) + MainContainer.fairyAll;
        }
        else
        {
            coin_fairyAmountText.text = "0" + MainContainer.fairyAll;
        }
    }
    void showTime()
    {
        float allTime = Time.timeSinceLevelLoad;//5400s
        gameTime = allTime;
        int hour = (int)allTime / 3600;//1.5h --1 
        hour %= 24;
        int minute = (int)allTime % 3600 / 60;//30min
        minute %= 60;
        int second = (int)allTime % 60;
        second %= 60;
        gameTimeText.text = Utility.getTwoNum(hour) + ":" + Utility.getTwoNum(minute) + ":" + Utility.getTwoNum(second);
    }
    void Update()
    {
        showTime();
        showCoins();
        WipeIn();
        WipeOut();  
        if ((!GameManager2._instance.hasOver1) && (!GameManager2._instance.hasOver2)&& (!TimelineManager2._instance.animPaused))
        {
            if (mainCanvas.alpha == 1 && Input.GetKeyUp(KeyCode.Escape))
            {
                Pause();
            }
        }
    }
    void Pause()
    {
        FirstPersonAIO._instance.canShoot = false;
        BatteryAIO._instance.canShoot = false;
        Cursor.lockState = CursorLockMode.None; Cursor.visible = true;        
        AudioManager._instance.bgmPlayer.Pause();
        Utility.EnableCanvas(pauseCanvas,0f);
        Time.timeScale = 0f;
    }

    IEnumerator EnableShoot()
    {
        yield return new WaitForSeconds(1f);
        FirstPersonAIO._instance.canShoot = true;
        BatteryAIO._instance.canShoot = true;
    }

    public void  ButtonDown(string name)
    {
        AudioManager._instance.PlayEffect("click");
        switch (name)
        {
            case "Restart":
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case "Resume":
                Time.timeScale = 1f;
                AudioManager._instance.bgmPlayer.UnPause();                
                Utility.DisableCanvas(pauseCanvas,0f);
                StartCoroutine(EnableShoot());
                Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;
                break;
            case "Quit":               
                Time.timeScale = 1;                
                SceneManager.LoadScene("0_start_select");
                Save._instance.SaveIkaCoinsAndTime(ikaCoinsNum, gameTime);
                break;
            case "Level1Restart":
                GameManager2._instance.LevelRestart();
                break;
        }
    }

    public void SetCoinText(int coinCount)
    {
        ikaCoinsNum = coinCount;
        //data panel
        coinText.text = Utility.getThreeNum(coinCount);        
    }
    public void ResetSlider()
    {
        waveSlider.value = 0;
        lifeSlider.value = 1;
    }
}
