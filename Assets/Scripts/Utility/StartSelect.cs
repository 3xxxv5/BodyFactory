using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StartSelect : MonoBehaviour
{
    public  CanvasGroup volumeCanvas;
    Image volumeSlider;
    public Sprite[] volumeSprites;
    int volumeIndex = 2;//0~5，一共6档，初始为2档
    Dictionary<int, float> volumeDic = new Dictionary<int, float>(6);
    bool isOpen = true;
    public  Button rightEarBtn;
    public Button luneBtn;
    public Button stomachBtn;
    public Image fadeImage;
 
    void Start()
    {
        fadeImage.color = Color.black;
        fadeImage.DOFade(0f,1f);
        InitMusicSettings();
    }
    private void Update()
    {
        Utility.ChangeVolume();
    }

    void InitMusicSettings()
    {
        AudioManager._instance.PlayeBGM("start");
        //设置音量界面       
        volumeSlider = volumeCanvas.transform.Find("volumeSlider").GetComponent<Image>();
        SetVolumeDic();
        volumeIndex = 2;
        volumeSlider.sprite = volumeSprites[volumeIndex];//设置初始档位的图片
        AudioManager._instance.SetPlayerVolume(volumeDic[volumeIndex]);//根据档位设置音量      
        Utility.DisableCanvas(volumeCanvas, 0f);
    }
    void SetVolumeDic()
    {
        volumeDic.Add(0, 0f);
        volumeDic.Add(1, 0.2f);
        volumeDic.Add(2, 0.4f);
        volumeDic.Add(3, 0.6f);
        volumeDic.Add(4, 0.8f);
        volumeDic.Add(5, 1f);
    }
 
    
    public void ButtonDown(string name)
    {
        AudioManager._instance.PlayEffect("click");
        switch (name)
        {
            case "sliderShow":
                if (isOpen)
                {
                    Utility.EnableCanvas(volumeCanvas, 0.5f);
                    isOpen = false;
                }
                else
                {
                    //Save
                    Save._instance.SaveAudioSettings(volumeIndex);
                    Utility.DisableCanvas(volumeCanvas, 0.5f);
                    isOpen = true;
                }
                break;
            case "changeVolume":
                volumeIndex = (volumeIndex + 1) % 6;
                AudioManager._instance.SetPlayerVolume(volumeDic[volumeIndex]);//根据档位设置音量：已经改变了，不会存在不保存的情况
                volumeSlider.sprite = volumeSprites[volumeIndex]; 
                break;
            case "luminanceSetting":

                break;
            case "home":
                OpenLevel("0_start");                
                break;
        }
    }
    void OpenLevel(string name)
    {
        float waitTime = 0.5f;
        fadeImage.DOFade(1f, waitTime);
        StartCoroutine(Utility.waitOpenLevel(waitTime, name));
    }


}
