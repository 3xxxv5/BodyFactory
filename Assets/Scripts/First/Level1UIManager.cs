using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Level1UIManager : MonoBehaviour
{
    public static Level1UIManager _instance { get; private set; }
    //pause panel
    [HideInInspector] public CanvasGroup pauseCanvas;
    Image fadeImage;
    //tip panel
    public  GameObject reflectPanel;
    public GameObject refractPanel;
    public Text coinText;
    private void Awake()
    {
        _instance = this;
        Init();
    }
    void Init()
    {

        AudioManager._instance.PlayeBGM("first");
        //pause panel
        pauseCanvas = transform.Find("PausePanel").GetComponent<CanvasGroup>();  
        Utility.DisableCanvas(pauseCanvas, 0f);
        coinText.text = Utility.getThreeNum(0);
        reflectPanel.SetActive(false);
        refractPanel.SetActive(false);
    }
    void Start()
    {
        fadeImage = transform.Find("fadeImage").GetComponent<Image>();
        fadeImage.color = Color.black;
        fadeImage.DOFade(0f, 1f);
    }

    // Update is called once per frame

    void Update()
    {
        Utility.ChangeVolume();
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }
    }

    void Pause()
    {
        AudioManager._instance.bgmPlayer.Pause();
        Utility.EnableCanvas(pauseCanvas, 0f);
        Time.timeScale = 0f;
    }

    public void ButtonDown(string name)
    {
        AudioManager._instance.PlayEffect("click");
        switch (name)
        {
            case "Restart":
                Time.timeScale = 1f;               
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case "Resume":
                Time.timeScale = 1f;              
                AudioManager._instance.bgmPlayer.UnPause();
                Utility.DisableCanvas(pauseCanvas, 0f);
                break;
            case "Quit":
                Time.timeScale = 1;               
                SceneManager.LoadScene("0_start");
                break;
        }
    }

    public void ShowTip(FairySorts fairySorts)
    {
        if (fairySorts == FairySorts.Reflect)
        {
            reflectPanel.SetActive(true);
        }
        else
        {
            refractPanel.SetActive(true);
        }
        
    }

    public void DisableTip(FairySorts fairySorts)
    {
        if (fairySorts == FairySorts.Reflect)
        {
            reflectPanel.SetActive(false);
        }
        else
        {
            refractPanel.SetActive(false);
        }              
    }
}

