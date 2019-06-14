using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public enum FairySorts
{
    Reflect,
    Refract,
    FixedReflect,
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
    public static string getTwoNum(int num)
    {
        if (num < 10)
        {
            return "0" + num.ToString();
        }
        else
        {
            return num.ToString();
        }
    }

}
