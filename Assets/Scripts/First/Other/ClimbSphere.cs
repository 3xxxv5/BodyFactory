using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbSphere : MonoBehaviour {
    Hair_PlayerMove playerMove;
    Transform tipBoard;
    ClimbStopSphere climbStop;
	// Use this for initialization
	void Start () {
        playerMove = GameObject.FindWithTag("Player").GetComponent<Hair_PlayerMove>();
        tipBoard = transform.Find("tipBoard");
        tipBoard.gameObject.SetActive(false);
        climbStop = transform.parent.Find("climbStopSphere").GetComponent<ClimbStopSphere>();
	}	

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            playerMove.canClimb = true;
            tipBoard.gameObject.SetActive(true);
            climbStop.cubeBoard.GetComponent<Collider>().isTrigger = true;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            tipBoard.gameObject.SetActive(false);
        }
    }
}
