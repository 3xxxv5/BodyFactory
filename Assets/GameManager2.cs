using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager2 : MonoBehaviour
{
    public static int level1HpBase =2;
    public static int level1Hp;
    public CanvasGroup overCanvas;
    public Text GoDieText;
    // Start is called before the first frame update
    void Start()
    {       
        Utility.DisableCanvas(overCanvas);
        Level1_Init();
    }
    void Level1_Init()
    {
        level1Hp = level1HpBase;
        //主角位置

        //楼层开启

        //Wave数组选用
    }


    void Update()
    {
        CheckLevel1Over();
        GoDieText.text = (level1HpBase - level1Hp).ToString();
    }

    void CheckLevel1Over()
    {
        if (level1Hp <= 0)
        {
            //输了
            //AudioManager._instance.PlayEffect("");
            Level1Over();
        }
    }
    void Level1Over()
    {
        FirstPersonAIO.GameOver();
        //出现UI 
        Utility.EnableCanvas(overCanvas,1f);
    }
    public void ButtonDown(string name)
    {
        switch (name)
        {
            case "restart":
                Level1Restart();
                break;
            case "lastLevel":
                ToLastLevel();
                break;
        }
    }
    void Level1Restart()
    {
        FirstPersonAIO.enableCameraMovement = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void ToLastLevel()
    {
        SceneManager.LoadScene("1_official");
    }

}
