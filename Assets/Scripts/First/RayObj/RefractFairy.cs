using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractFairy : Fairy
{
    protected Hair_PlayerMove playerMove;
    RefractRayEmitter[] refractRays;
    void Awake () {
        base.Init();
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<Hair_PlayerMove>();
        InitRefractRays();
    }
    void InitRefractRays()
    {
        refractRays = transform.GetComponentsInChildren<RefractRayEmitter>();

        //分为与X轴上下各45°的两道光线，两道光线之间为90度，仅能绕Y轴旋转，旋转角度只有4个（90°转一次）
        refractRays[0].dirs = new Vector3[4]
        {
            new Vector3(0,1,-1),new Vector3(-1,1,0),
            new Vector3(0,1,1),new Vector3(1, 1, 0),
        };
        refractRays[1].dirs = new Vector3[4]
        {
                new Vector3(0,-1,-1), new Vector3(-1,-1,0 ),
                new Vector3(0,-1,1),   new Vector3(1, -1, 0)
        };

        //确定起点
        for (int i = 0; i < 2; i++)
        {
            refractRays[i].startPoint = transform.position;
        }
    }
    
    protected override void FollowTarget()
    {
        base.FollowTarget();
        if (!isPicked)
        {
            transform.position = Vector3.Lerp(transform.position, playerMove.gameObject.transform.position + transform.right*0.5f+Vector3.up*0.5f, Time.deltaTime);
            transform.forward = Vector3.Lerp(transform.forward, playerMove.transform.forward, Time.deltaTime);
        }
    }
    protected override void RotateFairy()
    {
        if (truePicked && Input.GetKeyUp(KeyCode.R))
        {
            for (int i = 0; i < 2; i++)
            {
                //绕Y轴旋转，只有4个角度
                refractRays[i].index++;
                refractRays[i].index %= 4;
            }
        }
    }

    protected override void RenderRay()
    {
        base.RenderRay();
        for (int i = 0; i < 2; i++)
        {
            if (rayHited)
            {
                refractRays[i].lineRenderer.enabled = true;
            }
            else
            {
                refractRays[i].lineRenderer.enabled = false;
            }
        }
    }

    protected override void MoveFairy()
    {        
        //显示可移动的点阵
        ShowMovePoint();

        if (!truePicked) return;
        Vector3 playerPos = playerMove.transform.position;
        if (canMove)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
            if (Vector3.Distance(transform.position, targetPos) < 0.02f)
            {
                canMove = false;
            }
        }
        else
        {
            //移动
            if (Input.GetKeyUp(KeyCode.W)) InputResponse(playerMove.transform.up);
            if (Input.GetKeyUp(KeyCode.S)) InputResponse(playerMove.transform.up * (-1));
            if (Input.GetKeyUp(KeyCode.A)) InputResponse(playerMove.transform.right * (-1));
            if (Input.GetKeyUp(KeyCode.D)) InputResponse(playerMove.transform.right);
            if (Input.GetKeyUp(KeyCode.Q)) InputResponse(playerMove.transform.forward * (-1));
            if (Input.GetKeyUp(KeyCode.E)) InputResponse(playerMove.transform.forward);
            //控制角色在坐标点上
            float y = Mathf.Clamp(Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(playerPos.y - 1), Mathf.RoundToInt(playerPos.y + 2));
            float x = Mathf.Clamp(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(playerPos.x - 2), Mathf.RoundToInt(playerPos.x + 2));
            float z = Mathf.Clamp(Mathf.RoundToInt(transform.position.z), Mathf.RoundToInt(playerPos.z - 2), Mathf.RoundToInt(playerPos.z + 2));
            transform.position = new Vector3(x, y, z);
        }
    }

    protected override void ShowMovePoint()
    {
        if (truePicked)
        {
            int y = Mathf.RoundToInt(transform.position.y); int x = Mathf.RoundToInt(transform.position.x);
            int z = Mathf.RoundToInt(transform.position.z); Vector3 ori = new Vector3(x, y, z);
            Vector3 playerPos = playerMove.transform.position;
            //y
            SetPointsPos(2, 1, playerPos.y, yArray, yShow, Vector3.up, ori.y, ori);
            //x
            SetPointsPos(2, 2, playerPos.x, xArray, xShow, Vector3.right, ori.x, ori);
            //z
            SetPointsPos(2, 2, playerPos.z, zArray, zShow, Vector3.forward, ori.z, ori);
        }
        else
        {
            foreach (var i in pointsObjects)
            {
                Destroy(i);
            }
            pointsObjects.Clear();
            InitPointsArray();
        }
    }
}
