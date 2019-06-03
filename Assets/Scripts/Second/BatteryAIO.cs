using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BatteryAIO : MonoBehaviour
{
    #region 变量
    public static BatteryAIO _instance { get; private set; }

    //Look Settings
    [Header("Look Settings")]
    [Space(8)]
    public bool enableCameraMovement = true;
    public Vector3 rotationRange = new Vector3(45,90, Mathf.Infinity);
    [Range(0.01f, 100)]    public float mouseSensitivity = 10f;
    [Range(0.01f, 100)]    public float dampingTime = 0.05f;   
    public bool relativeMotionMode = true;
    public bool lockAndHideMouse = true;
    public Transform playerCamera;
    private Vector3 targetAngles;
    private Vector3 followAngles;
    private Vector3 followVelocity;
    private Vector3 originalRotation;

    //Shoot Settings
    [Header("Shoot Settings")]
    [Space(8)]
    [HideInInspector] public bool canShoot = true; 

    LayerMask monsterMask;
    LayerMask wallMask;
    Vector3 targetPos;
    [HideInInspector] public bool gameOver = false;
    GameObject bullet;
   

    //Shell Settings
    [Header("Shell Settings")]
    [Space(8)]
    public int shotMaxAmount = 3;
    public int shotIndex=0;

    Transform puffer;
    Vector3 pufferInitPosition;
    Quaternion pufferInitRotation;
    Vector3 cameraInitPosition;
    Quaternion cameraInitRotation;

    public  Pearl pearl;
    #endregion

    private void Awake()
    {
        _instance = this;
        //Look Settings - Awake
        originalRotation = transform.localRotation.eulerAngles;
        puffer = transform.Find("BatteryCamera/puffer");
        pufferInitPosition = puffer.transform.localPosition;
        pufferInitRotation = puffer.transform.localRotation;
        cameraInitPosition = playerCamera.localPosition;
        cameraInitRotation = playerCamera.localRotation;

        //shell settings-Awake
        //shoot settings- Awake
        enableCameraMovement = false; canShoot = false;
        bullet = Resources.Load<GameObject>("Prefabs/bullet");
    }
    public  void ResetBatteryPos()
    {
        playerCamera.localPosition = cameraInitPosition;
        playerCamera.localRotation = cameraInitRotation;
        if (puffer != null)
        {
            puffer.localPosition = pufferInitPosition;
            puffer.localRotation = pufferInitRotation;
        }
    }

  
    private void Update()
    {

        #region Look Settings - Update
        if (enableCameraMovement)
        {          
            float mouseXInput ,  mouseYInput;
            if (relativeMotionMode)
            {
                mouseXInput = Input.GetAxis("Mouse Y");
                mouseYInput = Input.GetAxis("Mouse X");
                if (targetAngles.y > 180) { targetAngles.y -= 360; followAngles.y -= 360; } else if (targetAngles.y < -180) { targetAngles.y += 360; followAngles.y += 360; }
                if (targetAngles.x > 180) { targetAngles.x -= 360; followAngles.x -= 360; } else if (targetAngles.x < -180) { targetAngles.x += 360; followAngles.x += 360; }
                targetAngles.y += mouseYInput * mouseSensitivity;
                targetAngles.x += mouseXInput * mouseSensitivity;
                targetAngles.y = Mathf.Clamp(targetAngles.y, -0.5f * rotationRange.z, 0.5f * rotationRange.z);
                targetAngles.x = Mathf.Clamp(targetAngles.x, -0.5f * rotationRange.x, 0.5f * rotationRange.y);
            }
            else
            {
                mouseXInput = Input.mousePosition.y;
                mouseYInput = Input.mousePosition.x;
                targetAngles.y = Mathf.Lerp(rotationRange.y * -0.5f, rotationRange.y * 0.5f, mouseXInput / Screen.width);
                targetAngles.x = Mathf.Lerp(rotationRange.x * -0.5f, rotationRange.x * 0.5f, mouseXInput / Screen.height);
            }
            followAngles = Vector3.SmoothDamp(followAngles, targetAngles, ref followVelocity, dampingTime);
            playerCamera.localRotation = Quaternion.Euler(-followAngles.x + originalRotation.x + cameraInitRotation.eulerAngles.x, 0, 0);
            transform.localRotation = Quaternion.Euler(0, followAngles.y + originalRotation.y, 0);
        }
      
        #endregion

        #region Shoot Settings - Update        
        if (!gameOver&&canShoot)
        {
            Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;

            if (pearl == null) return;
            if (pearl.firstOk|| pearl.secondOk|| pearl.thirdOk)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    Shoot();
                    SetShotCount();
                }
            }
        }
        #endregion

        if (Input.GetKeyUp(KeyCode.E))
        {
            GameManager2._instance.Change2PlayerView();

        }
    }   
    void SetShotCount()
    {
        if (pearl == null) return;
        if (pearl.firstOk)
        {
            pearl.firstOk = false;
            Level2UIManager._instance.circleProgress[0].fillAmount = 1;
        }
        else if(pearl.secondOk)
        {
            pearl.secondOk = false;
            Level2UIManager._instance.circleProgress[1].fillAmount = 1;
        }
        else if (pearl.thirdOk)
        {
            pearl.thirdOk = false;
            Level2UIManager._instance.circleProgress[2].fillAmount = 1;
        }
    }
    void Shoot()
    {
        Camera cam = playerCamera.GetComponent<Camera>();
        Vector3 centerPoint = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 2));
        GameObject bulletGo = Instantiate(bullet, centerPoint + playerCamera.transform.forward * 10, Quaternion.identity);
        bulletGo.GetComponent<Rigidbody>().velocity = playerCamera.transform.forward * 200;
        if (bulletGo != null) { Destroy(bulletGo, 5f); }

    }
    public void GameOver()
    {
        gameOver = true;
        enableCameraMovement = false;
        Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
    }

}


