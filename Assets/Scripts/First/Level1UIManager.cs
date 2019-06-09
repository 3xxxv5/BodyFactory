using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using taecg.tools;

public class Level1UIManager : MonoBehaviour
{
    public static Level1UIManager _instance { get; private set; }
    //pause panel
    [HideInInspector] public CanvasGroup pauseCanvas;
    public  Text coin_fairyAmountText;
    public  Text coin_ikaAmountText;
    public  Text gameTimeText;
    public Transform coins;
    //tip panel
    public  GameObject reflectPanel;
    public GameObject refractPanel;
    //进场
    Image fadeImage;
    public CircleWipe circleWipe;
    CanvasGroup mainCanvas;
    bool canWipeIn = false;
    bool canWipeOut = false;

    public Text coinText;
    private void Awake()
    {
        _instance = this;
        Init();
    }
    void Init()
    {

        AudioManager._instance.PlayeBGM("first");
        //pause panel
        pauseCanvas = transform.Find("PausePanel").GetComponent<CanvasGroup>();  
        Utility.DisableCanvas(pauseCanvas, 0f);
        coinText.text = Utility.getThreeNum(0);
        reflectPanel.SetActive(false);
        refractPanel.SetActive(false);
        print("coinFariy的总数："+(coins.childCount).ToString());
    
    }
    void Start()
    {
        fadeImage = transform.Find("fadeImage").GetComponent<Image>();
        //fadeImage.color = Color.black;
        //fadeImage.DOFade(0f, 1f);
        mainCanvas = transform.GetComponent<CanvasGroup>();
        mainCanvas.alpha = 0;
        circleWipe.Value = 0;
        canWipeIn = true;
    }
    void wipeIn()
    {
        if (canWipeIn)
        {
            circleWipe.Value = Mathf.Lerp(circleWipe.Value, 1, Time.deltaTime);
            if (circleWipe.Value > 0.85)
            {
                canWipeIn = false;
                circleWipe.Value = 1;
                mainCanvas.alpha = 1;
            }
        }
    }
    void wipeOut()
    {
        if (canWipeOut)
        {
            mainCanvas.alpha = 0;
            circleWipe.Value = Mathf.Lerp(circleWipe.Value, 0, Time.deltaTime);
            if (circleWipe.Value < 0.1)
            {
                canWipeOut = false;
                circleWipe.Value = 0;
            }
        }
    }

    // Update is called once per frame

    void Update()
    {
        wipeIn();
        wipeOut();
        Utility.ChangeVolume();
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }
    }

    void Pause()
    {
        AudioManager._instance.bgmPlayer.Pause();
        Utility.EnableCanvas(pauseCanvas, 0f);
        Time.timeScale = 0f;
    }

    public void ButtonDown(string name)
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
                Utility.DisableCanvas(pauseCanvas, 0f);
                break;
            case "Quit":
                Time.timeScale = 1;               
                SceneManager.LoadScene("0_start");
                break;
        }
    }

    public void ShowTip(FairySorts fairySorts)
    {
        if (fairySorts == FairySorts.Reflect)
        {
            reflectPanel.SetActive(true);
        }
        else
        {
            refractPanel.SetActive(true);
        }
        
    }

    public void DisableTip(FairySorts fairySorts)
    {
        if (fairySorts == FairySorts.Reflect)
        {
            reflectPanel.SetActive(false);
        }
        else
        {
            refractPanel.SetActive(false);
        }              
    }
}

