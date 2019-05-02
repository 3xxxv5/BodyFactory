using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCake : Monster {
    GameObject cakeChild;
    public int childrenAmount = 5;
    public float waitTime = 8f;
    public float maxDistance = 8f;
	void Start () {
        cakeChild= Resources.Load<GameObject>("Prefabs/m_cakeChild");
        StartCoroutine(SpawnChildren());
    }
	

	void Update () {

    }


    IEnumerator SpawnChildren()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            for (int i = 0; i < childrenAmount; i++)
            {
                float randomX = Random.Range(-maxDistance, maxDistance);
                float randomY = Random.Range(-maxDistance, maxDistance);
                float randomZ= Random.Range(-maxDistance, maxDistance);
                Instantiate(cakeChild,transform.localPosition+new Vector3(randomX,randomY,randomZ),Quaternion.identity);
            }
        }
    }
}
