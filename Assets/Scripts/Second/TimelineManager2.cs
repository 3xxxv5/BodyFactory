using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;

public class TimelineManager2 : MonoBehaviour
{
    public static TimelineManager2 _instance { get; private set; }
    public PlayableDirector level1Clip;
    public Camera level1Camera;
    public PlayableDirector level2Clip;
    public Camera level2Camera;
    public Camera level3Camera;
    public GameObject DoorChips;
    [HideInInspector]public bool animPaused = false;
    bool hasShowCursor = false;

    private void Awake()
    {
        _instance = this;
        Utility.DisableCamera(level1Camera.transform);
        Utility.DisableCamera(level2Camera.transform);
        Utility.DisableCamera(level3Camera.transform);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!hasShowCursor)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                hasShowCursor = true;
            }
        }
    }
    void TurnBlack()
    {
        Level2UIManager._instance.fadeImage.color = Color.clear;
        Level2UIManager._instance.fadeImage.DOFade(1, Level2UIManager._instance.toBlackTime);
    }
    void TurnClear()
    {
        Level2UIManager._instance.fadeImage.DOFade(0, 2F);
    }
    public void TurnWhite()
    {
        Level2UIManager._instance.whiteImage.color = new Color(1,1,1,0);
        Level2UIManager._instance.whiteImage.DOFade(1f, 1f);
    }
    public void TurnWhiteClear()
    {
        Level2UIManager._instance.whiteImage.DOFade(0f, 1f);
    }
    void OpenAnimCamera(Camera animCamera)
    {
        Utility.DisableCamera(FirstPersonAIO._instance.transform);
        Utility.EnableCamera(animCamera.transform);
    }
    void OpenPlayerCamera(Camera animCamera)
    {
        Utility.DisableCamera(animCamera.transform);
        Utility.EnableCamera(FirstPersonAIO._instance.transform);
    }

    public IEnumerator Level1WinAnim(PlayableDirector clip)
    {
        animPaused = true;
        TurnBlack();
        yield return new WaitForSeconds(1);
        OpenAnimCamera(level1Camera);
        clip.Play();
        TurnClear();
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(((float)clip.duration - 1) * 0.9f);
        TurnBlack();
        yield return new WaitForSeconds(1);
        OpenPlayerCamera(level1Camera);
        GameManager2._instance.ChangeSea();
        Level2UIManager._instance.ResetSlider();
        TurnClear();
        yield return new WaitForSeconds(1);
        GameManager2._instance.Level2_Init();
        FirstPersonAIO._instance.enableCameraMovement = true;
        animPaused = false;
    }
    public IEnumerator Level2WinAnim()
    {
        animPaused = true;
        TurnBlack();
        yield return new WaitForSeconds(1f);
        OpenAnimCamera(level2Camera);
        DragonManager._instance.Level2Show();//电鳗出现
        yield return new WaitForSeconds(2f);
        TurnClear();
        yield return new WaitForSeconds(4f);
        TurnBlack();
        yield return new WaitForSeconds(1f);
        OpenPlayerCamera(level2Camera);
        TurnClear();
        Destroy(DoorChips);
        yield return new WaitForSeconds(1f);
        GameManager2._instance.Level3_Init();
        animPaused = false;
    }
    public IEnumerator level3WinAnim()
    {
        animPaused = true;
        TurnBlack();
        yield return new WaitForSeconds(1f);
        OpenAnimCamera(level3Camera);
        DragonManager._instance.dragon.Dead();//电鳗死亡动画
        TurnClear();
        yield return new WaitForSeconds(3f);

        //进入下一关
        Level2UIManager._instance.canWipeOut = true;
        yield return new WaitForSeconds(3f);
        GameManager2._instance.ToNextLevel();
        animPaused = false;
    }
}
