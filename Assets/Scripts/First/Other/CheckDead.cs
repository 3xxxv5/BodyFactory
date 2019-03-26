using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDead : MonoBehaviour {
    GameObject player;
    public  CheckPoint[] checkPoints;
    bool isDead = false;
    float minY = -0.5f;
    int index = 0;
    bool hasOops = false;
	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        checkPoints = transform.GetComponentsInChildren<CheckPoint>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckFall();
        if (isDead)
        {
            CheckPointsIndex();
            StartCoroutine(ResetPlayer());
            isDead = false;
        }
    }

    void CheckFall()
    {
        if (player == null)
        {
            isDead = true;
            return;
        }
        if (player.transform.position.y < minY)
        {
            isDead = true;
        }
        else
        {
            isDead = false;
        }
    }
    void CheckPointsIndex()
    {
        for (int i=0;i<checkPoints.Length;i++)
        {
            if (!checkPoints[i].hasPass)
            {
                index =Mathf.Clamp( i - 1, 0, checkPoints.Length-1);
                break;
            }
        }
    }

    IEnumerator ResetPlayer()
    {
        if (!hasOops)
        {
            AudioManager._instance.PlayEffect("oops");
            hasOops = true;
        }

        yield return new WaitForSeconds(1);
        player.transform.position = checkPoints[index].transform.position;
        hasOops = false;
    }
}
