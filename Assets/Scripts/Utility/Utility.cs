using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Utility : MonoBehaviour
{
    public static void DisableCanvas(CanvasGroup canvas)
    {
        canvas.alpha = 0;
        canvas.interactable = false;
        canvas.gameObject.SetActive(false);
    }
    public static void EnableCanvas(CanvasGroup canvas,float time)
    {
        canvas.gameObject.SetActive(true);
        canvas.DOFade(1,time);
        canvas.interactable = true;

    }
}
