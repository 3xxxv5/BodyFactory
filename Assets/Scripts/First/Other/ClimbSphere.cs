using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbSphere : MonoBehaviour {
    Hair_PlayerMove playerMove;
    Transform tipBoard;
    ClimbStopSphere climbStop;
    Transform creeper;
	// Use this for initialization
	void Start () {
        playerMove = GameObject.FindWithTag("Player").GetComponent<Hair_PlayerMove>();
        tipBoard = transform.Find("tipBoard");
        tipBoard.gameObject.SetActive(false);
        climbStop = transform.parent.Find("climbStopSphere").GetComponent<ClimbStopSphere>();
        creeper = transform.parent.Find("creeper");
	}	

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            if (creeper.GetComponent<Renderer>().material.GetFloat("_dissolveAmount")<0.9f) return;//向上的藤已经出现，且主角进入可爬行区域，设置状态为可爬行
            playerMove.canClimb = true;
            tipBoard.gameObject.SetActive(true);
            for (int i = 0; i < climbStop.cubeColliders.Length; i++)
            {
                climbStop.cubeColliders[i].isTrigger = true;
            }           
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            playerMove.canClimb = false;//出爬行区域，设置状态为不可爬行
            tipBoard.gameObject.SetActive(false);
        }
    }
}
