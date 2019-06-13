using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonManager : MonoBehaviour
{
    public static DragonManager _instance { get; private set; }
    Animator dragonRoadAnim;
    public Dragon dragon;
    private void Awake()
    {
        _instance = this;   
    }
    void Start()
    {
        dragonRoadAnim = GetComponent<Animator>();    
    }


    void Update()
    {
        
    }
    public IEnumerator Level1Run(float toWhite, float toClear, float waitTime)
    {
        AudioManager._instance.PlayEffect("");//电鳗受伤的声音
        FirstPersonAIO._instance.enableCameraMovement = false;
        Level2UIManager._instance.whiteImage.DOFade(1f, toWhite);

        yield return new WaitForSeconds(toWhite + waitTime);
       
        Level2UIManager._instance.whiteImage.DOFade(0f, toClear);
        AudioManager._instance.PlayEffect("");//电鳗逃跑的声音

        GameManager2._instance.level1foodNum -= 1;
        dragon.gameObject.SetActive(false);//电鳗隐藏
        yield return new WaitForSeconds(toClear);

        FirstPersonAIO._instance.enableCameraMovement = true;
    }
    public void Level2Show()
    {
        dragon.gameObject.SetActive(true);
        dragonRoadAnim.SetBool("collide",true);
    }
}
