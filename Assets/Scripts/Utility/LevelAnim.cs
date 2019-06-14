using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using taecg.tools;

public class LevelAnim : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Image skipImage;
    float time = 0;
    public  float timer = 2f;//需要按压的时间
    bool canSkip = true;
    public string nextLevel;
    public CircleWipe circleWipe;
    bool canWipeIn = false;
    bool canWipeOut = false;
    float endValue = 0.55f;
    private float wipeOutSpeed=0.5f;
    private float wipeInSpeed = 0.5f;

    void Start()
    {
        skipImage.gameObject.SetActive(false);
        skipImage.fillAmount = 0;
        AudioManager._instance.PlayeBGM("");
        circleWipe.Value = 0;
        canWipeIn = true;
        StartCoroutine(PlayVideo());
    }
    IEnumerator PlayVideo()
    {
        videoPlayer.Play();
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds((float)videoPlayer.length-1);
        OpenLevel(nextLevel);
    }
    void wipeIn()
    {
        if (canWipeIn)
        {
            circleWipe.Value += Time.deltaTime* wipeInSpeed;
            if (circleWipe.Value > endValue)
            {
                canWipeIn = false;
                circleWipe.Value = endValue;
            }
        }
    }
    void wipeOut()
    {
        if (canWipeOut)
        {
            circleWipe.Value -=Time.deltaTime* wipeOutSpeed;//从1-0，大约需要1s
            if (circleWipe.Value < 0.01f)
            {
                canWipeOut = false;
                circleWipe.Value = 0;
            }
        }
    }

    void FixedUpdate()
    {
        wipeIn();
        wipeOut();
        videoPlayer.SetDirectAudioVolume(0, AudioManager._instance.bgmPlayer.volume);
        if (canSkip)
        {
            if (Input.anyKey)
            {
                skipImage.gameObject.SetActive(true);
                time += Time.deltaTime;
                skipImage.fillAmount = time / timer;
                if (time >= timer)
                {
                    time = 0;
                    StopCoroutine(PlayVideo());                   
                    OpenLevel(nextLevel);
                    canSkip = false;
                }
            }
            else
            {
                time = 0;
                skipImage.gameObject.SetActive(false);
            }
        }
    }

     void OpenLevel(string name)
    {
        float waitTime =(endValue+0.1f)/wipeOutSpeed;
        canWipeOut = true;
        StartCoroutine(Utility.waitOpenLevel(waitTime, name));
    }
}
