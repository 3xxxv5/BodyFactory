using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[AddComponentMenu("First Person AIO")]
public class FirstPersonAIO : MonoBehaviour {

    public static FirstPersonAIO _instance {
        get; private set;
    }
    #region Look Settings
    [Header("Look Settings")]
    [Space(8)]

    public bool enableCameraMovement=true;
    public Vector2 rotationRange = new Vector2(170, Mathf.Infinity);
   [Range(0.01f, 100)]   public float mouseSensitivity = 10f;
   [Range(0.01f, 100)]    public float dampingTime = 0.05f;
    public bool lockAndHideMouse = true;
    public Transform playerCamera;
    private Vector3 targetAngles;
    private Vector3 followAngles;
    private Vector3 followVelocity;
    private Vector3 originalRotation;
    [Space(15)]

    #endregion

    #region Shoot Settings
    [Header("Shoot Settings")]
    [Space(8)]
    [HideInInspector] public bool canShoot = true;
    //input time constriant
    private float timer=2f;
    float tictock = 0;
    //clip
    AudioClip wallClip;
    AudioClip monsterClip;
    AudioClip flyClip;
    //raycast
    Vector3 p1;
    LayerMask monsterMask;
    LayerMask wallMask ;
    public bool canTransfer = false;
    Vector3 targetPos;
    public float transferSpeed=5;
    public float qteTransferSpeed = 1;
    float moveSpeed;
    private  float maxDistanceToWall = 0.1f;
    public Transform imageEffectCube;
    GameObject effectGo;
    [HideInInspector]public  bool gameOver = false;
    //qte
    bool hitMonster = false;
    bool hitWall = false;
    bool hitBattery = false;
    bool hitCenterBall = false;
    bool hitDragonBlood = false;
    [HideInInspector] public bool hasQte = false;
    [HideInInspector] public bool qteWin = false;
    int endIndex = -1;
    //Camera Movement
    public Transform nearPos;
    public Transform farPos;
    public float camMoveSpeed = 2;
    bool goFar = false;
    bool goNear = false;
    bool turnAround = false;
    float degree = 0;
    public float rotateSpeed;
    public float distance = 20f;
    public  bool attackDragon = false;
    #endregion

    private void Awake()
    {
        _instance = this;
        originalRotation = transform.localRotation.eulerAngles;
    }
    
    private void Start()
    {
        //Look Settings - Start
        enableCameraMovement = true;   
        lockAndHideMouse = true;
        if (lockAndHideMouse) { Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false; }

        //Shoot Settings - Start        
        wallClip= Resources.Load<AudioClip>("Sound/Effect/oops");
        monsterClip = Resources.Load<AudioClip>("Sound/Effect/refract");
        flyClip = Resources.Load<AudioClip>("Sound/Effect/fly");
        imageEffectCube.gameObject.SetActive(false);
        tictock = timer;  
    }

    private void Update()
    {
        #region Look Settings - Update

        if(enableCameraMovement)
        {
            float mouseXInput;            float mouseYInput;
            mouseXInput = Input.GetAxis("Mouse Y");
            mouseYInput = Input.GetAxis("Mouse X");
            if (targetAngles.y > 180) { targetAngles.y -= 360; followAngles.y -= 360; } else if (targetAngles.y < -180) { targetAngles.y += 360; followAngles.y += 360; }
            if (targetAngles.x > 180) { targetAngles.x -= 360; followAngles.x -= 360; } else if (targetAngles.x < -180) { targetAngles.x += 360; followAngles.x += 360; }
            targetAngles.y += mouseYInput * mouseSensitivity;
            targetAngles.x += mouseXInput * mouseSensitivity;
            targetAngles.y = Mathf.Clamp(targetAngles.y, -0.5f * rotationRange.y, 0.5f * rotationRange.y);
            targetAngles.x = Mathf.Clamp(targetAngles.x, -0.5f * rotationRange.x, 0.5f * rotationRange.x);
        }
        followAngles = Vector3.SmoothDamp(followAngles, targetAngles, ref followVelocity, dampingTime);
        transform.localRotation = Quaternion.Euler(-followAngles.x + originalRotation.x, followAngles.y + originalRotation.y, originalRotation.z);

        #endregion

        #region Shoot Settings - Update        
        if (!gameOver&&canShoot)
        {
            if (Camera.main == null) return;
            //发射一条从屏幕中点到摄像机方向的射线
            p1 = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 2));
            Ray monsterRay = new Ray(p1, Camera.main.transform.forward);
            Debug.DrawRay(p1, Camera.main.transform.forward, Color.green);
            //开启射线可以射到的层
            wallMask = 1 << LayerMask.NameToLayer("monster")
                | 1 << LayerMask.NameToLayer("wall")
                | 1 << LayerMask.NameToLayer("centerBall")
                | 1 << LayerMask.NameToLayer("dragon")
                | 1 << LayerMask.NameToLayer("battery");

            tictock += Time.deltaTime;
            if (tictock > timer)  //计时器限制，2s后才能发射下一次
            {
                if (Input.GetMouseButtonUp(0))
                {
                    //每一次发射前可以cleanUp变量
                    Shoot(monsterRay);//发射
                    tictock = 0;
                }
            }

            if (canTransfer)
            {
                Transfer();//因为是持续运动，所以需要在Update里执行
                CameraTransfer();
            }
        }    
        #endregion
    }

    void Transfer()//持续改变玩家位置，把玩家送到end
    {
        if (hasQte) moveSpeed = qteTransferSpeed;
        else moveSpeed = transferSpeed;

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
        if (Vector3.Distance(transform.position, targetPos) < maxDistanceToWall)
        {
            imageEffectCube.gameObject.SetActive(false);
            ShootCleanUp();
            enableCameraMovement = true;
            playerCamera.localPosition = nearPos.localPosition;
            playerCamera.rotation = nearPos.rotation;
            canTransfer = false;
        }       
    }
    void CameraTransfer()
    {
       
        if (goFar)
        {
            playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, farPos.localPosition, Time.deltaTime * camMoveSpeed);
            if (Vector3.Distance(playerCamera.localPosition, farPos.localPosition) < 0.1f)
            {
                goFar = false;
                goNear = true;               
            }
        }
      
        if (goNear)
        {
            playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, nearPos.localPosition, Time.deltaTime * camMoveSpeed);
            if (Vector3.Distance(playerCamera.localPosition, nearPos.localPosition) < 0.1f)
            {             
                goNear = false;
            }
        }
    }
    void ShootCleanUp()
    {
        hitWall = false; 
        hitMonster = false;
        hitDragonBlood = false;
        hitCenterBall = false;
        hitBattery = false;
        qteWin = false;
    }
    void Shoot(Ray monsterRay)
    {     
        RaycastHit[] monsterHits = Physics.RaycastAll(monsterRay, 3000, wallMask);  //射到的所有物体的数组
        if (monsterHits.Length == 1)//只射中了一个物体
        {
            switch (monsterHits[0].collider.gameObject.layer)
            {
                //monster
                case 18: AudioManager._instance.PlayEffect("gun"); ShootManager._instance.canChange2Battery = true; break;
               // wall
                case 19: ShootTransfer(monsterHits[0]); ShootManager._instance.canChange2Battery = true; break;
                //battery
                case 25: hitBattery = true; ShootTransfer(monsterHits[0]); break;      
            }
            print("飞到了：" + monsterHits[0].transform.gameObject.name);
        }
        else if (monsterHits.Length > 1)//射中1个以上的物体，标记是否射中指定物体
        {
            ShootManager._instance.canChange2Battery = true;
            for (int i = 0; i < monsterHits.Length; i++)
            {
                switch (monsterHits[i].collider.gameObject.layer)
                {
                    case 25: hitBattery = true; endIndex = i; break;
                    case 19: hitWall = true; endIndex = i; break;
                    case 18: hitMonster = true; break;
                    case 23: hitCenterBall = true; break;
                    case 14: hitDragonBlood = true;break;
                }
            }
            if (hitWall && hitMonster)
            {
                if (hitCenterBall)
                {
                    hasQte = true;//用于设置Qte 状态下的移动速度
                    print("打中了centerBall：");
                    StartCoroutine(WuZei._instance.Qte(WuZei._instance.qteTime));
                }               
            }
            if (hitWall && hitDragonBlood)
            {
                hasQte = true;//用于设置Qte 状态下的移动速度
                print("打中了龙：");
                StartCoroutine(WuZei._instance.Qte(WuZei._instance.qteTime));
            }
            if (hitWall|| hitBattery)
            {
                print("飞到了："+monsterHits[endIndex].transform.gameObject.name);
                ShootTransfer(monsterHits[endIndex]);//不管有没有射中，tranfer总是要进行的。player只管移动。移动中发生的事情由乌贼的控制
            }
        }
    }
    void ShootTransfer(RaycastHit wallHit)
    {
        AudioManager._instance.PlayEffect("fly");    

        canTransfer = true;        //开启移动开关
        targetPos = wallHit.point;//设置落地地点
        imageEffectCube.gameObject.SetActive(true);//设置屏幕特效

        enableCameraMovement = false;//禁止相机运动
        goFar = true;

    }
    public void GameOver()
    {
        gameOver = true;
        enableCameraMovement = false;
        Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
    }
}


