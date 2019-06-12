using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbStopSphere : MonoBehaviour {

    Hair_PlayerMove playerMove;
    public  Transform cubeBoard;
    [HideInInspector]public  Collider[] cubeColliders;
    void Start()
    {
        playerMove = GameObject.FindWithTag("Player").GetComponent<Hair_PlayerMove>();       
     
        cubeColliders = cubeBoard.GetComponentsInChildren<Collider>();
        for(int i = 0; i < cubeColliders.Length; i++)
        {
            cubeColliders[i].isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            playerMove.isClimbing = false;//已经爬上去，设置状态为不在爬行
            for (int i = 0; i < cubeColliders.Length; i++)
            {
                cubeColliders[i].isTrigger = false;
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {

    }
}
