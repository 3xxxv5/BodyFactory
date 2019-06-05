using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public enum FairySorts
{
    Reflect,
    Refract
}
public class Utility : MonoBehaviour
{

    public static float waitTime = 1f;
    public static void DisableCanvas(CanvasGroup canvas,float time)
    {
        canvas.DOFade(0, time);
        canvas.interactable = false;
        canvas.gameObject.SetActive(false);
    }
    public static void EnableCanvas(CanvasGroup canvas,float time)
    {
        canvas.gameObject.SetActive(true);
        canvas.DOFade(1,time);
        canvas.interactable = true;
    }
    public static IEnumerator waitToDisable(CanvasGroup canvas, float time)
    {
        yield return new WaitForSeconds(time);
        canvas.gameObject.SetActive(false);
    }
    public static void DisableCamera(Transform trans)
    {
        trans.GetComponentInChildren<Camera>().enabled = false;
    }
    public static void EnableCamera(Transform trans)
    {
        trans.GetComponentInChildren<Camera>().enabled = true;
    }
    public static IEnumerator waitOpenLevel(float time, string name)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(name);
    }
    public static void ChangeVolume()
    {
        if (AudioManager._instance.reduceVol)
        {
            AudioManager._instance.bgmPlayer.volume = Mathf.Lerp(AudioManager._instance.bgmPlayer.volume, 0f, Time.deltaTime * 2);
            print(AudioManager._instance.bgmPlayer.volume);
            if (AudioManager._instance.bgmPlayer.volume < 0.05f)
            {
                AudioManager._instance.reduceVol = false;
            }
        }
        if (AudioManager._instance.increaseVol)
        {
            AudioManager._instance.bgmPlayer.volume = Mathf.Lerp(AudioManager._instance.bgmPlayer.volume, AudioManager._instance.saveVolume, Time.deltaTime * 2);
            if ((AudioManager._instance.saveVolume-AudioManager._instance.bgmPlayer.volume) < 0.05f)
            {
                AudioManager._instance.increaseVol = false;
            }
        }
    }         
    public static string getThreeNum(int num)
    {
        if (num< 10) {
            return "00" + num.ToString();
        }else if (num < 100)
        {
            return "0" + num.ToString();
        }
        else
        {
            return num.ToString();
        }
    }

}
