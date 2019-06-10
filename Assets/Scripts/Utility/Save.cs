using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save: MonoBehaviour{

    private static Save instance;
    public static Save _instance
    {
        get
        {
            if (!instance)
            {
                instance = new Save();              
            }
            return instance as Save;
        }
    }

    void Awake()
    {
        instance = this;
    }
	public void SaveLevel()
    {
        string activeLevel = SceneManager.GetActiveScene().name;
        string[] activeLevelStrs = activeLevel.Split('_');
        //存储最新进度
        if (activeLevelStrs[0] == "Level")
        {
            PlayerPrefs.SetInt("isFirstIn", 0);
            PlayerPrefs.SetString("saveLevel", SceneManager.GetActiveScene().name);
        }
        if(activeLevelStrs[0] == "Help")
        {
            PlayerPrefs.SetString("saveHelpLevel", SceneManager.GetActiveScene().name);
            print(PlayerPrefs.GetString("saveHelpLevel"));
        }

        //存储最大进度
        string saveLevel = PlayerPrefs.GetString("saveLevel");
        string[] saveStrs = saveLevel.Split('_');
        if (PlayerPrefs.HasKey("mostSaveLevel"))
        {
            string mostSaveLevel = PlayerPrefs.GetString("mostSaveLevel");
            string[] mostSaveStrs = mostSaveLevel.Split('_');

            if (saveStrs[0] == "Level")//save是关卡，才有比较的需要
            {
                int saveNum = Convert.ToInt32(saveStrs[1]);
                int mostNum = Convert.ToInt32(mostSaveStrs[1]);
                if (saveNum > mostNum)
                {
                    PlayerPrefs.SetString("mostSaveLevel", saveLevel);//更新
                }
                else if (saveNum == mostNum)
                {
                    int saveNum_2 = Convert.ToInt32(saveStrs[2]);
                    int mostNum_2 = Convert.ToInt32(mostSaveStrs[2]);
                    if (saveNum_2 > mostNum_2)
                    {                       
                        PlayerPrefs.SetString("mostSaveLevel", saveLevel);//更新
                    }
                }
            }
        }      
        else
        {
            if (saveStrs[0] == "Level")
            {
                PlayerPrefs.SetString("mostSaveLevel", saveLevel);//存储第一次的最新进度
            }
        }
    }

    public void SaveAudioSettings(int volumeIndex)
    {
        PlayerPrefs.SetInt("volume", volumeIndex);
    }
     
    public void SavePlaySecondMovie(int hasPlay)
    {
        PlayerPrefs.SetInt("hasPlay", hasPlay);
    }
    public void SaveBackState()
    {
        //确定一下是返回的，不是第一次打开就行
        string[] nameStr = SceneManager.GetActiveScene().name.Split('_');
        PlayerPrefs.SetString("BackState", "Back");
       
    }

    public bool SaveInfiniteScore(int totalScore)
    {        
        if(!PlayerPrefs.HasKey("MaxScore"))
        {
            PlayerPrefs.SetInt("MaxScore", totalScore);
            return true;
        }
        else
        {
            if (totalScore > PlayerPrefs.GetInt("MaxScore"))
            {
                PlayerPrefs.SetInt("MaxScore", totalScore);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void SaveFairyCoinsAndTime(int fairyCoins,float fairyTime)
    {
        //coins
        PlayerPrefs.SetInt(MainContainer.fairyCoins, fairyCoins);
        if (PlayerPrefs.HasKey(MainContainer.maxFairyCoins))
        {
            if (fairyCoins > PlayerPrefs.GetInt(MainContainer.maxFairyCoins))
            {
                PlayerPrefs.SetInt(MainContainer.maxFairyCoins, fairyCoins);
            }
        }
        else
        {
            PlayerPrefs.SetInt(MainContainer.maxFairyCoins, fairyCoins);
        }
        //time
        PlayerPrefs.SetFloat(MainContainer.fairyTime, fairyTime);
    }
    public void SaveIkaCoinsAndTime(int ikaCoins,float ikaTime)
    {
        PlayerPrefs.SetInt(MainContainer.ikaCoins, ikaCoins);
        if (PlayerPrefs.HasKey(MainContainer.maxIkaCoins))
        {
            if (ikaCoins > PlayerPrefs.GetInt(MainContainer.maxIkaCoins))
            {
                PlayerPrefs.SetInt(MainContainer.maxIkaCoins,ikaCoins);
            }
        }
        else
        {
            PlayerPrefs.SetInt(MainContainer.maxIkaCoins, ikaCoins);
        }
        //time
        PlayerPrefs.SetFloat(MainContainer.ikaTime, ikaTime);
    }
    public void SaveGameTime(float fairyTime,float ikaTime)
    {
        float gameTime = fairyTime + ikaTime;
        if (PlayerPrefs.HasKey(MainContainer.minGameTime))
        {
            if (gameTime < PlayerPrefs.GetFloat(MainContainer.minGameTime))
            {
                PlayerPrefs.SetFloat(MainContainer.minGameTime, gameTime);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(MainContainer.minGameTime, gameTime);
        }
    }
    public void DeleteFairyCoinsAndTime()
    {
        PlayerPrefs.DeleteKey(MainContainer.fairyCoins);
        PlayerPrefs.DeleteKey(MainContainer.fairyTime);
    }
    public void DeleteIkaCoinsAndTime()
    {
        PlayerPrefs.DeleteKey(MainContainer.ikaCoins);
        PlayerPrefs.DeleteKey(MainContainer.ikaTime);
    }
}
