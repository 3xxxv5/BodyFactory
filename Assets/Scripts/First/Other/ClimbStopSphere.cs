using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbStopSphere : MonoBehaviour {

    Hair_PlayerMove playerMove;
    [HideInInspector]
    public  Transform cubeBoard;

    void Start()
    {
        playerMove = GameObject.FindWithTag("Player").GetComponent<Hair_PlayerMove>();
        cubeBoard = transform.Find("cubeBoard");
        cubeBoard.GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            playerMove.canClimb = false;
            cubeBoard.GetComponent<Collider>().isTrigger = false;
        }
    }
    private void OnTriggerExit(Collider col)
    {

    }
}
