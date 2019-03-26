using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 每2s就发射一波怪物
/// </summary>
public class EmitManager : MonoBehaviour {
    public  float timer = 4;
    float time = 0;
    public int randomRange =20;
    public  GameObject monster;
	// Use this for initialization
	void Start () {
        if(monster==null)
        monster = Resources.Load("Prefabs/monster") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (time < timer)
        {
            time += Time.deltaTime;
        }
        else
        {
            int random_x = Random.Range(-randomRange, randomRange);
            int random_z = Random.Range(-randomRange, randomRange);
            Instantiate(monster,transform.position+new Vector3(random_x, 0, random_z),transform.rotation);
            time = 0;
        }
	}
}
