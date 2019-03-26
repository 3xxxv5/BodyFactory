using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(Camera))]
public class CameraPick : MonoBehaviour
{
    #region variables
    public LayerMask fairyLayerMask = -1;//-1代表everything 如果公共属性是一个枚举值，会有选择
    float targetingRayLength = Mathf.Infinity;//射线长度为无穷大
    Camera cam;//不局限于主摄像机
    public  Transform gameCast = null;//设置空对象
    public  List<Transform> pickedObjs;//设置空对象
    bool pickFixed = false;
    GameObject player;
    GameObject exitPickText;
    DOTweenAnimation tween;
    public  Transform canvas;
    public Color enterColor;
    public Color pickColor;
    public Color pickNotNowColor;

    enum PlayerState
    {
        Frozen,
        Normal
    }
    PlayerState playerState=PlayerState.Normal;
    enum FairyColor
    {
        EnterColor,
        PickColor,
        PickNotNowColor,
        NormalColor
    }
    #endregion

    void Start()
    {
        #region targetCast - Start
        cam = GetComponent<Camera>();
        exitPickText = GameObject.FindWithTag("exitPickText");
        tween = exitPickText.GetComponent<DOTweenAnimation>();
        exitPickText.SetActive(false);
        pickedObjs.Clear();
        canvas.gameObject.SetActive(true);
        #endregion

        #region tip&controlPlayer - Start
        player = GameObject.FindGameObjectWithTag("Player");
        #endregion
    }

    void Update()
    {
        if (player.GetComponent<Hair_PlayerMove>().animPaused) return;//动画静止

        TargetingRayCast();
        ShowTipAndControlPlayer();
    }

    #region TargetCast

    /// <summary>
    /// 点选小精灵
    /// </summary>
    public void TargetingRayCast()
    {
        Vector3 mp = Input.mousePosition;
        if (cam == null) return;
        RaycastHit hitInfo;
        Ray ray = cam.ScreenPointToRay(new Vector3(mp.x, mp.y, 0f));
        if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, targetingRayLength, fairyLayerMask.value))
        {
            if (gameCast != null) NonFirstCast(hitInfo.transform);
            else FirstCast(hitInfo.transform);
        }
    }

    #region 逻辑函数
    /// <summary>
    /// 鼠标第一次移到某个物体上
    /// </summary>
    /// <param name="trans"></param>
    void FirstCast(Transform trans)
    {
        ChangeFairy_Color(trans, FairyColor.EnterColor); //变色        
        gameCast = trans;//更新gamecast
    }

    /// <summary>
    /// 如果鼠标已经移过某个物体，并且保存在了gamecast中
    /// </summary>
    /// <param name="hitTrans"></param>
    void NonFirstCast(Transform hitTrans)
    {
        if (hitTrans == gameCast) ClickLastCast();       
        else CastOtherObj(hitTrans.transform);
    }

    /// <summary>
    ///鼠标点击物体
    /// </summary>
    void ClickLastCast()
    {
        if (Input.GetMouseButtonUp(0))
        {
            AudioManager._instance.PlayEffect("click");
            if ( ! pickedObjs.Contains(gameCast))
            {
                FirstClick();
            }
            else
            {
                NonFirstClick();
            }
        }
    }

    /// <summary>
    /// 此时鼠标移向另一个物体
    /// </summary>
    /// <param name="trans"></param>
    void CastOtherObj(Transform trans)
    {       
        if ( ! ifFairyPicked(trans, "isPicked"))
            ChangeFairy_Color(trans, FairyColor.EnterColor);//【1】变当前为蓝           
        if ( ! ifFairyPicked(gameCast, "isPicked"))
            ChangeFairy_Color(gameCast, FairyColor.NormalColor);//【2】变上一个为无色
        gameCast = trans;//【3】更新gameCast
    }

    /// <summary>
    /// 取消点选Fairy
    /// </summary>
    /// <param name="trans"></param>
    void CancelPick(Transform trans)
    {
        ChangeFairy_Color(trans, FairyColor.NormalColor);//【1】取消自己
        ChangeFairyState(trans, false, false);
        pickedObjs.Remove(trans);
       
        if (pickedObjs.Count == 0) //【2】取消后的处理
        {
            playerState = PlayerState.Normal;
        }
        else
        {
            ChangeFairy_Color(pickedObjs[0], FairyColor.PickColor);
            ChangeFairyState(pickedObjs[0], true, true);
        }
    }

    /// <summary>
    /// 第一次点击该物体
    /// </summary>
    void FirstClick()
    {
        //先变List中的其他物体
        //若在此之前已经点击过其他物体,则其他物体变成 【picked but not nowPicked】 状态
        if (pickedObjs.Count > 0)
        {
            for (int i = 0; i < pickedObjs.Count; i++)
            {
                ChangeFairy_Color(pickedObjs[i], FairyColor.PickNotNowColor);
                ChangeFairyState(pickedObjs[i], true, false);
                # region 专门针对固定反射小精灵做的处理
                if (! pickedObjs[i].tag.Equals("fixReflectObj"))
                {
                        
                }
                else
                {//
                    
                }
                #endregion
            }
        }
        //再变该物体，然后加入List
        playerState = PlayerState.Frozen;
        pickedObjs.Add(gameCast);
        ChangeFairy_Color(gameCast, FairyColor.PickColor);
        ChangeFairyState(gameCast, true, true);
    }
    /// <summary>
    /// 不是第一次点击，可能是退出选择，也可能是更改当前pick
    /// </summary>
    void NonFirstClick()
    {
        if (ifFairyPicked(gameCast, "truePicked"))
        {
            CancelPick(gameCast);//取消点选                               
        }
        else if( ifFairyPicked(gameCast, "isPicked"))//改变当前pick
        {
            for (int i = 0; i < pickedObjs.Count; i++)//【1】变List其他物体
            {
                ChangeFairy_Color(pickedObjs[i], FairyColor.PickNotNowColor);
                ChangeFairyState(pickedObjs[i], true, false);
            }
            ChangeFairy_Color(gameCast, FairyColor.PickColor);//【2】变当前物体，再加入List
            ChangeFairyState(gameCast, true, true);
      
        }
    }
    #endregion

    #region 辅助函数
    void ChangeFairy_Color(Transform transform, FairyColor color)
    {
        switch (color)
        {
            case FairyColor.EnterColor:
                transform.GetComponent<HighlightableObject>().ConstantOn(enterColor);
                break;
            case FairyColor.PickColor:
                transform.GetComponent<HighlightableObject>().ConstantOn(pickColor);
                break;
            case FairyColor.PickNotNowColor:
                transform.GetComponent<HighlightableObject>().ConstantOn(pickNotNowColor);
                break;
            case FairyColor.NormalColor:
                transform.GetComponent<HighlightableObject>().ConstantOff();
                break;
        }
    }
    bool ifFairyPicked(Transform transform, string myBool)
    {
        if (LayerMask.LayerToName(transform.gameObject.layer) == "reflectObj")
        {
            if (myBool == "isPicked")
            {
                return transform.GetComponent<ReflectFairy>().isPicked;
            }
            else
            {
                return transform.GetComponent<ReflectFairy>().truePicked;
            }

        }
        else if (LayerMask.LayerToName(transform.gameObject.layer) == "refractObj")
        {
            if (myBool == "isPicked")
            {
                return transform.GetComponent<RefractFairy>().isPicked;
            }
            else
            {
                return transform.GetComponent<RefractFairy>().truePicked;
            }
        }
        else
        {
            print("射中的层设置出错");
            return false;
        }
    }
    void ChangeFairyState(Transform gameCheck, bool pickBool, bool truePick)
    {
        if (LayerMask.LayerToName(gameCheck.gameObject.layer) == "reflectObj")
        {
            gameCheck.GetComponent<ReflectFairy>().isPicked = pickBool;
            gameCheck.GetComponent<ReflectFairy>().truePicked = truePick;
        }
        else if (LayerMask.LayerToName(gameCheck.gameObject.layer) == "refractObj")
        {
            gameCheck.GetComponent<RefractFairy>().isPicked = pickBool;
            gameCheck.GetComponent<RefractFairy>().truePicked = truePick;
        }
    }
    #endregion

    #endregion

    #region  ShowTip&ControlPlayer
    /// <summary>
    /// 旋转/平移UI、玩家停止行走
    /// </summary>
    void ShowTipAndControlPlayer()
    {
        if (playerState==PlayerState.Frozen)
        {
            exitPickText.SetActive(true);
            tween.DOPlay();
            PlayerMoveControl(false);
        }
        else
        {
            tween.DOPause();
            exitPickText.SetActive(false);
            PlayerMoveControl(true);
        }
    }

    void PlayerMoveControl(bool myBool)
    {
        player.GetComponent<Hair_PlayerMove>().canMove = myBool;
    }
    #endregion


}

