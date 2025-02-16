﻿using System.Collections;
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
    [HideInInspector]public  float gameTime;
    public Transform coins;
    //tip panel
    public  GameObject reflectPanel;
    public GameObject refractPanel;
    public GameObject fixedReflectPanel;
    public GameObject tipPanel;
    //进场
    Image fadeImage;
    public CircleWipe circleWipe;
    CanvasGroup mainCanvas;
    [HideInInspector]public  bool canWipeIn = false;
    [HideInInspector]public  bool canWipeOut = false;
    float endValue = 0.55f;
    private float wipeOutSpeed = 0.5f;
    private float wipeInSpeed = 0.5f;

    public Text coinText;
    [HideInInspector] public int fairyCoinsNum;

    private void Awake()
    {
        _instance = this;
        Init();
    }
    void Init()
    {
        Save._instance.DeleteFairyCoinsAndTime();
        gameTime = 0;
        AudioManager._instance.PlayeBGM("first");
        //pause panel
        pauseCanvas = transform.Find("PausePanel").GetComponent<CanvasGroup>();  
        Utility.DisableCanvas(pauseCanvas, 0f);
        coinText.text = Utility.getThreeNum(0);
        reflectPanel.SetActive(false);
        refractPanel.SetActive(false);
        fixedReflectPanel.SetActive(false);
        print("coinFariy的总数："+(coins.childCount).ToString());
        tipPanel.SetActive(true);
    
    }
    void Start()
    {
        fadeImage = transform.Find("fadeImage").GetComponent<Image>();
        mainCanvas = transform.GetComponent<CanvasGroup>();
        mainCanvas.alpha = 0;
        circleWipe.Value = 0;
        canWipeIn = true;
    }
    void wipeIn()
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
    void wipeOut()
    {
        if (canWipeOut)
        {
            circleWipe.Value -= Time.deltaTime * wipeOutSpeed;//从1-0，大约需要1s
            if (circleWipe.Value < 0.01f)
            {
                canWipeOut = false;
                circleWipe.Value = 0;
            }
        }
    }

    // Update is called once per frame
    void showCoins()
    {
        coin_fairyAmountText.text = fairyCoinsNum.ToString() + MainContainer.fairyAll;
        if (PlayerPrefs.HasKey(MainContainer.ikaCoins))
        {
            coin_ikaAmountText.text = PlayerPrefs.GetInt(MainContainer.ikaCoins) + MainContainer.ikaAll;
        }
        else
        {
            coin_ikaAmountText.text = "0"+ MainContainer.ikaAll;
        }
    }
    void showTime()
    {
        float allTime = Time.timeSinceLevelLoad;//5400s
        gameTime = allTime;
        int hour = (int)allTime/3600;//1.5h --1 
        hour %= 24;      
        int minute = (int)allTime%3600/60;//30min
        minute %= 60;
        int second = (int)allTime % 60;
        second %= 60;
        gameTimeText.text = Utility.getTwoNum(hour) + ":" + Utility.getTwoNum(minute) + ":" + Utility.getTwoNum(second);
    }
    void Update()
    {
        showCoins();
        showTime();      
        wipeIn();
        wipeOut();
        if (mainCanvas.alpha==1&&Input.GetKeyUp(KeyCode.Escape))
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
                SceneManager.LoadScene("0_start_select");
                Save._instance.SaveFairyCoinsAndTime(fairyCoinsNum,gameTime);
                break;
        }
    }

    public void ShowTip(FairySorts fairySorts, bool rayHited)
    {
        if (fairySorts == FairySorts.Reflect)
        {
            reflectPanel.SetActive(true);
            if (rayHited)
            {
                reflectPanel.transform.Find("hitPanel").gameObject.SetActive(true);
            }
            else
            {
                reflectPanel.transform.Find("hitPanel").gameObject.SetActive(false);
            }
        }
        else if (fairySorts == FairySorts.Refract)
        {
            refractPanel.SetActive(true);
            if (rayHited)
            {
                refractPanel.transform.Find("hitPanel").gameObject.SetActive(true);
            }
            else
            {
                refractPanel.transform.Find("hitPanel").gameObject.SetActive(false);
            }
        }
        else if (fairySorts == FairySorts.FixedReflect)
        {
            fixedReflectPanel.SetActive(true);
            if (rayHited)
            {
                fixedReflectPanel.transform.Find("hitPanel").gameObject.SetActive(true);
            }
            else
            {
                fixedReflectPanel.transform.Find("hitPanel").gameObject.SetActive(false);
            }
        }
        
    }

    public void DisableTip(FairySorts fairySorts)
    {
        if (fairySorts == FairySorts.Reflect)
        {
            reflectPanel.transform.Find("hitPanel").gameObject.SetActive(false);
            reflectPanel.SetActive(false);          
        }
        else if (fairySorts == FairySorts.Refract)
        {
            refractPanel.transform.Find("hitPanel").gameObject.SetActive(false);
            refractPanel.SetActive(false);        
        }
        else if (fairySorts == FairySorts.FixedReflect)
        {
            fixedReflectPanel.transform.Find("hitPanel").gameObject.SetActive(false);
            fixedReflectPanel.SetActive(false);
        }
    }

    public void SetCoinText(int coinCount)
    {
        fairyCoinsNum = coinCount;
        coinText.text = Utility.getThreeNum(coinCount);
    }
       

}

