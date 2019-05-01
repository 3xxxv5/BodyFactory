using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Utility : MonoBehaviour
{
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
    public static void DisableCamera(Transform trans)
    {
        trans.GetComponentInChildren<Camera>().enabled = false;
        trans.GetComponentInChildren<AudioListener>().enabled = false;
    }
    public static void EnableCamera(Transform trans)
    {
        trans.GetComponentInChildren<Camera>().enabled = true;
        trans.GetComponentInChildren<AudioListener>().enabled = true;
    }
}
