using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StartUIManager : MonoBehaviour
{
    Image fadeImage;
    CanvasGroup volumeCanvas;
    Image volumeSlider;
    public Sprite[] volumeSprites;
    int volumeIndex = 2;//0~5，一共6档，初始为2档
    Dictionary<int, float> volumeDic = new Dictionary<int, float>(6);
    bool isOpen = true;
    Button rightEarBtn;
    // Start is called before the first frame update
    void Start()
    {
        fadeImage = transform.Find("fadeImage").GetComponent<Image>();
        AudioManager._instance.PlayeBGM("first");
        //Volume Slider Init
        rightEarBtn = transform.Find("SelectPanel/body/rightEarBtn").GetComponent<Button>();
        volumeCanvas = transform.Find("SelectPanel/body/volumePanel").GetComponent<CanvasGroup>();
        volumeSlider = volumeCanvas.transform.Find("volumeSlider").GetComponent<Image>();
        SetVolumeDic();
        //根据存档设置音量，若无存档，初始化一个
        if (PlayerPrefs.HasKey("volume"))
        {
            int saveIndex = PlayerPrefs.GetInt("volume");
            volumeSlider.sprite = volumeSprites[saveIndex];
            AudioManager._instance.SetPlayerVolume(volumeDic[saveIndex]);
        }
        else
        {
            volumeIndex = 2;
            volumeSlider.sprite = volumeSprites[volumeIndex];//设置初始档位的图片
            AudioManager._instance.SetPlayerVolume(volumeDic[volumeIndex]);//根据档位设置音量
        }
        Utility.DisableCanvas(volumeCanvas,0f);
    }
    void SetVolumeDic()
    {
        volumeDic.Add(0,0f);
        volumeDic.Add(1, 0.2f);
        volumeDic.Add(2, 0.4f);
        volumeDic.Add(3, 0.6f);
        volumeDic.Add(4, 0.8f);
        volumeDic.Add(5, 1f);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonDown(string name)
    {
        switch (name)
        {
            case "firstLevel":
                OpenLevel(1);
                break;
            case "secondLevel":
                OpenLevel(2);
                break;
            case "thirdLevel":
                OpenLevel(3);
                break;
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
                }

                break;
            case "changeVolume":
                volumeIndex = (volumeIndex + 1) % 6;
                AudioManager._instance.SetPlayerVolume(volumeDic[volumeIndex]);//根据档位设置音量：已经改变了，不会存在不保存的情况
                volumeSlider.sprite = volumeSprites[volumeIndex]; 
                break;
            case "luminanceSetting":

                break;
            case "Quit":
                //index = UnityEngine.Random.Range(1, 5);
                //AudioManager._instance.PlayEffect("click" + index.ToString());
                //if (isLevelMode)
                //{
                //    selcetMode.ChangeSwipeMenu(true);
                //}
                //else
                //{
                //    Application.Quit();
                //}
                break;
        }
    }
    void OpenLevel(int num)
    {
        float waitTime = 0.5f;
        fadeImage.DOFade(1f, waitTime);
        StartCoroutine(waitOpenLevel(waitTime, num));
    }
    IEnumerator waitOpenLevel(float time,int num)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(num.ToString()+"_official");
    }
}
