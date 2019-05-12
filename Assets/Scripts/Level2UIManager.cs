using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using DG.Tweening;

public class Level2UIManager : MonoBehaviour
{
    public static Level2UIManager _instance { get; private set; }
    //data panel
    [HideInInspector] public CanvasGroup dataCanvas;
    [HideInInspector] public Slider waveSlider;
    [HideInInspector] public Slider lifeSlider;
    public Image[] circleProgress;
    //cross panel
    [HideInInspector] public CanvasGroup crossCanvas;
    [HideInInspector] public  Image simpleCross;
    [HideInInspector] public Image batteryCross;
    //over panel
    [HideInInspector] public CanvasGroup overCanvas;
    [HideInInspector] public Image fadeImage;
    public float toBlackTime = 1f;
    public float toClearTime = 1f;
    //pause panel
    [HideInInspector] public CanvasGroup pauseCanvas;
    //other
    [HideInInspector] public Animator lifeAnim;
    //viedo
    private CanvasGroup canvasGroup;
    public VideoPlayer videoPlayer;
    public Camera videoCamera;
    private void Awake()
    {
        _instance = this;
        Init();
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    void Init()
    {
        canvasGroup = GetComponent<CanvasGroup>(); Save._instance.SavePlaySecondMovie(1);
        if (PlayerPrefs.HasKey("hasPlay"))
        {
            AudioManager._instance.PlayeBGM("second");
            videoCamera.gameObject.SetActive(false);
        }
        else
        {
            //播放视频
            StartCoroutine(playMovie());
        }


        for (int i = 0; i < circleProgress.Length; i++) circleProgress[i].fillAmount = 0;
        //pause panel
        pauseCanvas = transform.Find("PausePanel").GetComponent<CanvasGroup>();
        Utility.DisableCanvas(pauseCanvas,0f);

        //data panel
        dataCanvas = transform.Find("DataPanel").GetComponent<CanvasGroup>();
        waveSlider = dataCanvas.transform.Find("WaveSlider").GetComponent<Slider>();
        lifeSlider = dataCanvas.transform.Find("LifeSlider").GetComponent<Slider>();
     

      
        //over panel
        fadeImage = transform.Find("FadeImage").GetComponent<Image>();
        overCanvas = transform.Find("GameOverPanel").GetComponent<CanvasGroup>();
        Utility.DisableCanvas(overCanvas, 0f);

        //cross Panel
        crossCanvas = transform.Find("CrossPanel").GetComponent<CanvasGroup>();
        simpleCross = crossCanvas.transform.Find("simpleCross").GetComponent<Image>();
        batteryCross = crossCanvas.transform.Find("batteryCross").GetComponent<Image>();
        batteryCross.gameObject.SetActive(false);
        Utility.DisableCanvas(crossCanvas, 0f);
        Utility.EnableCanvas(crossCanvas, 1f);

    }
    IEnumerator playMovie()
    {
        AudioManager._instance.PlayeBGM("");
        AudioManager._instance.PlayEffect("");
        videoCamera.gameObject.SetActive(true);
        canvasGroup.DOFade(0f, 0.5f);
        canvasGroup.interactable = false;
        videoPlayer.playOnAwake = true;
        yield return new WaitForSeconds((float)videoPlayer.length);
        AudioManager._instance.PlayeBGM("second");
        canvasGroup.DOFade(1f, 0.5f);
        canvasGroup.interactable = true;
        videoCamera.gameObject.SetActive(false);
        Save._instance.SavePlaySecondMovie(1);
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager2._instance.hasOver)
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                Pause();
            }
        }
    }
    void Pause()
    {
        FirstPersonAIO._instance.canShoot = false;
        BatteryAIO._instance.canShoot = false;
        Cursor.lockState = CursorLockMode.None; Cursor.visible = true;        
        AudioManager._instance.bgmPlayer.Pause();
        Utility.EnableCanvas(pauseCanvas,0f);
        Time.timeScale = 0f;
    }

    IEnumerator EnableShoot()
    {
        yield return new WaitForSeconds(1f);
        FirstPersonAIO._instance.canShoot = true;
        BatteryAIO._instance.canShoot = true;
    }

    public void  ButtonDown(string name)
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
                Utility.DisableCanvas(pauseCanvas,0f);
                StartCoroutine(EnableShoot());
                Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;
                break;
            case "Quit":               
                Time.timeScale = 1;
                PlayerPrefs.SetInt("isFirstIn", 1);//是第一次进来
                Save._instance.SaveLevel();//存一下是新手关的第几关
                SceneManager.LoadScene("0_start");
                break;
        }
    }
}
