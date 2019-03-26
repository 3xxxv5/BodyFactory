using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 怪物被发射后，向下直线匀速运动
/// </summary>
public class Monster : MonoBehaviour {
    public  float dropSpeed=2f;
    public GameObject chips;
	// Use this for initialization
	void Start () {
        if (chips == null)
            chips = Resources.Load<GameObject>("Prefabs/m_donutChips");
    }
	
	// Update is called once per frame
	void Update () {
        //transform或者velocity
        transform.position = Vector3.Lerp(transform.position,transform.position+new Vector3(0,-1,0),Time.deltaTime*dropSpeed);
	}
}
