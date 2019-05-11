using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level2UIManager : MonoBehaviour
{
    public static Level2UIManager _instance { get; private set; }
    //data panel
    [HideInInspector] public CanvasGroup dataCanvas;
    [HideInInspector] public Text shotCountText;
    [HideInInspector] public Text reduceBuffText;
    [HideInInspector] public Slider waveSlider;
    [HideInInspector] public Slider lifeSlider;
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
        //pause panel
        pauseCanvas = transform.Find("PausePanel").GetComponent<CanvasGroup>();
        Utility.DisableCanvas(pauseCanvas,0f);

        //data panel
        dataCanvas = transform.Find("DataPanel").GetComponent<CanvasGroup>();
        waveSlider = dataCanvas.transform.Find("WaveSlider").GetComponent<Slider>();
        lifeSlider = dataCanvas.transform.Find("LifeSlider").GetComponent<Slider>();


        lifeAnim = lifeSlider.transform.Find("Handle Slide Area/Handle").GetComponent<Animator>();
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
    // Update is called once per frame
    void Update()
    {
        if (!GameManager2._instance.hasOver)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
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
        int index = Random.Range(1, 5);
        AudioManager._instance.PlayEffect("click" + index.ToString());
        AudioManager._instance.bgmPlayer.Pause();
        Utility.EnableCanvas(pauseCanvas,0f);
        Time.timeScale = 0f;
    }

    public void  ButtonDown(string name)
    {
        switch (name)
        {
            case "Restart":
                Time.timeScale = 1f;
                int index = Random.Range(1, 5);
                AudioManager._instance.PlayEffect("click" + index.ToString());
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case "Resume":
                Time.timeScale = 1f;
                index = Random.Range(1, 5);
                AudioManager._instance.PlayEffect("click" + index.ToString());
                AudioManager._instance.bgmPlayer.UnPause();                
                Utility.DisableCanvas(pauseCanvas,0f);
                FirstPersonAIO._instance.canShoot = false;
                BatteryAIO._instance.canShoot = false;
                Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;
                break;
            case "Quit":               
                Time.timeScale = 1;
                index = Random.Range(1, 5);
                AudioManager._instance.PlayEffect("click" + index.ToString());
                PlayerPrefs.SetInt("isFirstIn", 1);//是第一次进来
                Save._instance.SaveLevel();//存一下是新手关的第几关
                SceneManager.LoadScene("0_start");
                break;
        }
    }
}
