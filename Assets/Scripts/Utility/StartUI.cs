using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class StartUI : MonoBehaviour
{
    public Image fadeImage;

    void Start()
    {
        fadeImage.color = Color.black;
        fadeImage.DOFade(0f, 1f);
        AudioManager._instance.PlayeBGM("start");
    }
    private void Update()
    {
        Utility.ChangeVolume();
    }
    public void ButtonDown(string name)
    {
        AudioManager._instance.PlayEffect("click");
        switch (name)
        {
            case "start":
                OpenLevel("0_start_anim");
                break;           
            case "quit":
                Application.Quit();
                break;
        }
    }
    void OpenLevel(string name)
    {
        AudioManager._instance.saveVolume = AudioManager._instance.bgmPlayer.volume;
        AudioManager._instance.reduceVol = true;
        fadeImage.DOFade(1f, Utility.waitTime);
        StartCoroutine(Utility.waitOpenLevel(Utility.waitTime, name));
    }
    
}
