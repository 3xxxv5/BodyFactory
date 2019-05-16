using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LevelAnim : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Image fadeImage;
    public CanvasGroup skipPanel;
    public Slider skipSlider;
    float time = 0;
    public  float timer = 2f;//需要按压的时间
    bool canSkip = true;
    public string nextLevel;
    void Start()
    {
        fadeImage.color = Color.black;
        fadeImage.DOFade(0f, 1f);
        skipSlider.value = 0;
        Utility.DisableCanvas(skipPanel,0f);
        AudioManager._instance.PlayeBGM("");
        StartCoroutine(PlayVideo());
    }
    IEnumerator PlayVideo()
    {       
        videoPlayer.Play();
        yield return new WaitForSeconds((float)videoPlayer.length);
        OpenLevel(nextLevel);
    }
    // Update is called once per frame
    void Update()
    {
        Utility.ChangeVolume();
        videoPlayer.SetDirectAudioVolume(0, AudioManager._instance.bgmPlayer.volume);
        if (canSkip)
        {
            if (Input.anyKey)
            {
                Utility.EnableCanvas(skipPanel,0f);
                time += Time.deltaTime;
                skipSlider.value = time / timer;
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
                Utility.DisableCanvas(skipPanel, 0f);
            }
        }
    }

     void OpenLevel(string name)
    {
        float waitTime =0f;
        fadeImage.DOFade(1f, waitTime);
        StartCoroutine(Utility.waitOpenLevel(waitTime, name));
    }
}
