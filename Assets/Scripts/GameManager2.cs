using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables;
using DG.Tweening;
public class GameManager2 : MonoBehaviour
{
    #region 变量
    public static GameManager2 _instance { get; private set; }
    [Header("Level 1")]
    [Space(8)]
    public GameObject level1Collider;
    public int level1HpBase = 5;
    [HideInInspector]
    public int level1Hp;
    public PlayableDirector level1Clip;
    public Camera level1Camera;

    [Header("Level 2")]
    [Space(8)]
    public GameObject level2Collider;
    public int level2HpBase = 8;
    [HideInInspector]
    public int level2Hp;
    public PlayableDirector level2Clip;
    public Camera level2Camera;

    [Header("Level 3")]
    [Space(8)]
    public GameObject level3Collider;
    public int level3HpBase = 10;
    [HideInInspector]
    public int level3Hp;
    public PlayableDirector level3Clip;
    public Camera level3Camera;

    [Header("Other")]
    [Space(8)]
    public CanvasGroup overCanvas;
    public Text GoDieText;
    public Image blackPanel;
    bool hasOver = false;
    public float toBlackTime = 1f;
    public float toClearTime = 1f;
    #endregion

    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        blackPanel.color = Color.black;
        blackPanel.DOFade(0f,1f);
        Utility.DisableCanvas(overCanvas);
        Level1_Init();
    }
    void Level1_Init()
    {
        level1Hp = level1HpBase;
        Utility.DisableCamera(level1Camera.transform);
        //主角位置

        //楼层开启

        //Wave数组选用
    }


    void Update()
    {
        CheckLevel1Over();
        GoDieText.text = "Hp:"+level1Hp.ToString();
    }

    void CheckLevel1Over()
    {
        if (level1Hp <= 0)
        {
            Level1Over();
        }
    }
    public void CheckWin(int level)//发射完最后一波后检测
    {
        switch (level)
        {
            case 1:
                CheckLevel1Win();
                break;
            case 2:
                CheckLevel2Win();
                break;
            case 3:
                CheckLevel3Win();
                break;
        }
    }
    void Level1Over()
    {
        if (!hasOver)
        {
            AudioManager._instance.PlayEffect("oops");
            FirstPersonAIO._instance.GameOver();
            //出现UI 
            Utility.EnableCanvas(overCanvas, 1f);
            hasOver = true;
        }
    }
    public void CheckLevel1Win()
    {
        if (level1Hp >= 0)
        {
            AudioManager._instance.PlayEffect("reflect");
            //过场动画
            StartCoroutine(WinAnim(level1Clip));
        }
    }
    public void CheckLevel2Win()
    {
        if (level2Hp >= 0)
        {
            AudioManager._instance.PlayEffect("reflect");
            //过场动画
            StartCoroutine(WinAnim(level2Clip));
        }
    }
    public void CheckLevel3Win()
    {
        if (level3Hp >= 0)
        {
            AudioManager._instance.PlayEffect("reflect");
            //过场动画
            StartCoroutine(WinAnim(level3Clip));
        }
    }

    IEnumerator WinAnim(PlayableDirector clip)
    {
        //变黑
        blackPanel.DOFade(1, toBlackTime);
        yield return new WaitForSeconds(toBlackTime);
        Utility.DisableCamera(FirstPersonAIO._instance.transform);
        Utility.EnableCamera(level1Camera.transform);
        //全黑时开始播过场
        clip.Play();
        //同时开始播变白的动画
        blackPanel.DOFade(0, toClearTime);
        yield return new WaitForSeconds(toClearTime);
        //变白的动画播完时,还剩1.4s
        yield return new WaitForSeconds(((float)clip.duration-toClearTime)*0.9f);
        //播到还差20%结束时，开始变黑
        blackPanel.DOFade(1, toBlackTime);
        yield return new WaitForSeconds(toBlackTime);

        Utility.DisableCamera(level1Camera.transform);
        Utility.EnableCamera(FirstPersonAIO._instance.transform);

        blackPanel.DOFade(0, toClearTime);
        yield return new WaitForSeconds(toClearTime);
        

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
        FirstPersonAIO._instance.enableCameraMovement = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void ToLastLevel()
    {
        SceneManager.LoadScene("1_official");
    }

}
