using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class EndUIManager : MonoBehaviour
{
    public Text fairyCoinText;
    public Text ikaCoinText;
    public Text timeText;
    float gameTime;
    public Animator lastAnim;
    public static bool canQuit = false;
    public Image fadeImage;
    public Image bestIka;
    public Image bestFairy;
    public Image bestTime;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        ShowCoins();
        ShowTime();
        //best：

    }
    void Init()
    {
        fadeImage.color = Color.black;
        fadeImage.DOFade(0,1f);

        bestIka.gameObject.SetActive(false);
        bestFairy.gameObject.SetActive(false);
        bestTime.gameObject.SetActive(false);
    }
    void ShowCoins()
    {
        //ika coin
        if (PlayerPrefs.HasKey(MainContainer.ikaCoins))
        {
            ikaCoinText.text = PlayerPrefs.GetInt(MainContainer.ikaCoins).ToString() + "/28";
        }
        else
        {
            ikaCoinText.text = "0/28";
        }
        //fairy coin
        if (PlayerPrefs.HasKey(MainContainer.fairyCoins))
        {
            fairyCoinText.text = PlayerPrefs.GetInt(MainContainer.fairyCoins).ToString() + "/18";
        }
        else
        {
            fairyCoinText.text = "0/18";
        }
    }
    void ShowTime()
    {
        gameTime = 0;
        if (PlayerPrefs.HasKey(MainContainer.fairyTime))
        {
            gameTime += PlayerPrefs.GetFloat(MainContainer.fairyTime);
        }
        else
        {
            gameTime += 0;
        }
        if (PlayerPrefs.HasKey(MainContainer.ikaTime))
        {
            gameTime += PlayerPrefs.GetFloat(MainContainer.ikaTime);
        }
        else
        {
            gameTime += 0;
        }
        int hour = (int)gameTime / 3600;//1.5h --1 
        hour %= 24;
        int minute = (int)gameTime % 3600 / 60;//30min
        minute %= 60;
        int second = (int)gameTime % 60;
        second %= 60;
        timeText.text = Utility.getTwoNum(hour) + ":" + Utility.getTwoNum(minute) + ":" + Utility.getTwoNum(second);
    }
    void ShowBest()
    {
        //把当前分数和最好分数比较，如果相等就是的
        if (PlayerPrefs.HasKey(MainContainer.maxFairyCoins))
        {
            if (PlayerPrefs.GetInt(MainContainer.fairyCoins) == PlayerPrefs.GetInt(MainContainer.maxFairyCoins))
            {
                bestFairy.gameObject.SetActive(true);
            }
        }
        if (PlayerPrefs.HasKey(MainContainer.maxIkaCoins))
        {
            if (PlayerPrefs.GetInt(MainContainer.ikaCoins) == PlayerPrefs.GetInt(MainContainer.maxIkaCoins))
            {
                bestIka.gameObject.SetActive(true);
            }
        }
        if (PlayerPrefs.HasKey(MainContainer.maxIkaCoins))
        {
            if (PlayerPrefs.GetInt(MainContainer.ikaCoins) == PlayerPrefs.GetInt(MainContainer.maxIkaCoins))
            {
                bestIka.gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canQuit)
        {
            if (Input.anyKeyDown)
            {
                fadeImage.DOFade(1f, Utility.waitTime);
                StartCoroutine(Utility.waitOpenLevel(Utility.waitTime, "0_start"));
                canQuit = false;
            }
        }
    }
}
