using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StartUIManager : MonoBehaviour
{
    Image fadeImage;
    // Start is called before the first frame update
    void Start()
    {
        fadeImage = transform.Find("fadeImage").GetComponent<Image>();
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
            case "musicSetting":

                break;
            case "luminanceSetting":

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
