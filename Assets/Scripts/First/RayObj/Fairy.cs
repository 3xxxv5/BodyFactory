using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fairy : MonoBehaviour {

    protected Transform tipBoard;
    protected Transform rotateTip;
    Button rotateButton;
    public  GameObject movePoint;
    public bool rayHited = false;
    public bool isPicked = false;//已经被选中过，但可能不是当前正在处理的
    public bool truePicked = false;//被选中，且正在被操作
    public int cellSize = 1;
    public int hitRay = 0;
    Animator anim;
    protected bool canFollow = false;
    protected bool[] yShow = new bool[4] { false, false, false, false };
    protected bool[] xShow = new bool[5] { false, false, false, false, false };
    protected bool[] zShow = new bool[5] { false, false, false, false, false };
    protected bool oriShow = false;
    protected Vector3[] yArray = new Vector3[4];
    protected Vector3[] xArray = new Vector3[5];
    protected Vector3[] zArray = new Vector3[5];
    public  List<GameObject> pointsObjects;
    protected float moveSpeed = 5f;
    protected bool canMove = false;
    protected Vector3 targetPos;
    [HideInInspector]
    public  enum FairyState
    {
        Idle,
        Blossom,
        Unblossom
    }
    public FairyState fairyState = FairyState.Idle;

    protected virtual void Init()
    {
        tipBoard = transform.Find("tipBoard");
        rotateTip = tipBoard.Find("RotateTip");
        rotateButton = rotateTip.GetComponent<Button>();
        rotateButton.onClick.AddListener(RotateFairy);
        tipBoard.gameObject.SetActive(false);    
        rotateTip.gameObject.SetActive(false);
        anim = transform.GetComponentInChildren<Animator>();
        movePoint = Resources.Load<GameObject>("Prefabs/movePoint");
        pointsObjects = new List<GameObject>();
    }
    protected 
    void Update () {
        //显示提示板
        ShowTipBoard();
        //跟随主角
        FollowTarget();
        //渲染射线
        RenderRay();
        //选中并移动
        MoveFairy();       
    }

   protected void ShowTipBoard()
    {
        if (truePicked)
        {
            tipBoard.gameObject.SetActive(true);
            if (rayHited)       
                rotateTip.gameObject.SetActive(true);
            else
                rotateTip.gameObject.SetActive(false);               
        }
        else
        {
            tipBoard.gameObject.SetActive(false);     
        }       
    }

    protected virtual void FollowTarget()
    {
        if (!canFollow) return;
    }
   protected virtual void MoveFairy()
    {

    }

    protected virtual void ShowMovePoint()
    {

    }
    protected virtual void InputResponse(Vector3 dir)
    {
        AudioManager._instance.PlayEffect("move");
        targetPos = transform.position + dir;
        canMove = true;
    }
    protected virtual void SetPointsPos(int maxOffest, int minOffset, float baseAxis, Vector3[] axisArray, bool[] showArray, Vector3 uniform, float oriAxis, Vector3 ori)
    {
        int yMax = Mathf.RoundToInt(baseAxis + maxOffest) - Mathf.RoundToInt(oriAxis);
        int yMin = Mathf.RoundToInt(baseAxis - minOffset) - Mathf.RoundToInt(oriAxis);
        axisArray[0] = ori + uniform * yMax; axisArray[1] = ori + uniform * yMin;
        axisArray[2] = ori + uniform * (yMax - 1); axisArray[3] = ori + uniform * (yMin + 1);
        if (axisArray.Length > 4) axisArray[4] = ori + uniform * (yMin + 2);
        GameObject go;
        for (int i = 0; i < axisArray.Length; i++)
        {
            if (!showArray[i])
            {
                go = Instantiate(movePoint, axisArray[i], Quaternion.identity);
                if (axisArray[i] == ori)   Destroy(go);//删掉重复的点
                else   pointsObjects.Add(go);//在该坐标点生成一个
                showArray[i] = true;
            }
        }
        if (!oriShow)
        {
            GameObject go2 = Instantiate(movePoint, ori, Quaternion.identity);
            pointsObjects.Add(go2);
            oriShow = true;
        }
    }
    protected virtual void InitPointsArray()
    {
        for (int i = 0; i < yShow.Length; i++) yShow[i] = false;
        for (int i = 0; i < xShow.Length; i++) xShow[i] = false;
        for (int i = 0; i < zShow.Length; i++) zShow[i] = false;
    }
    protected virtual void RotateFairy()
    {

    }
    protected virtual void OverTurnFairy()
    {

    }
    protected virtual void RenderRay()
    {
        if (hitRay > 0)
        {
            rayHited = true;
            fairyState = FairyState.Blossom;            
        }
        else
        {
            rayHited = false;
            fairyState = FairyState.Unblossom;            
        }
        anim.SetInteger("FairyState", (int)fairyState);
    }

    
}
