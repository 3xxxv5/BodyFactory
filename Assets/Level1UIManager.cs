using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1UIManager : MonoBehaviour
{
    public static Level1UIManager _instance { get; private set; }
    //pause panel
    [HideInInspector] public CanvasGroup pauseCanvas;

    private void Awake()
    {
        _instance = this;
        Init();
    }
    void Init()
    {
        //pause panel
        pauseCanvas = transform.Find("PausePanel").GetComponent<CanvasGroup>();
        Utility.DisableCanvas(pauseCanvas, 0f);
    }
    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }
    }

    void Pause()
    {
        int index = Random.Range(1, 5);
        AudioManager._instance.PlayEffect("click" + index.ToString());
        AudioManager._instance.bgmPlayer.Pause();
        Utility.EnableCanvas(pauseCanvas, 0f);
        Time.timeScale = 0f;
    }

    public void ButtonDown(string name)
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
                Utility.DisableCanvas(pauseCanvas, 0f);
                break;
            case "Quit":
                Time.timeScale = 1;
                index = Random.Range(1, 5);
                AudioManager._instance.PlayEffect("click" + index.ToString());
                SceneManager.LoadScene("0_start");
                break;
        }
    }
}

