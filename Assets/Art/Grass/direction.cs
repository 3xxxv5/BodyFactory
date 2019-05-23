using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class direction : MonoBehaviour {
    public GameObject hero;
    private Renderer grassMaterial;

    // Use this for initialization
    private void Awake()
    {
        grassMaterial = GetComponent<Renderer>();
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector4 value;
        value =new Vector4(hero.transform.position.x, hero.transform.position.y, hero.transform.position.z, 1);
        //targetPos = grassMaterial.material.GetFloat();
        grassMaterial.material.SetVector("_heroPos", value);
    }
}
