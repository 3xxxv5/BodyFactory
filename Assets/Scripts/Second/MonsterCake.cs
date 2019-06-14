using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCake : Monster {
    GameObject cakeChild;
    int childrenAmount = 5;
    float waitTime = 8f;
    public float maxDistance = 12f;
    int index = 0;
	void Start () {
        cakeChild= Resources.Load<GameObject>("Prefabs/m_cakeChild");
        StartCoroutine(SpawnChildren());
    }

    IEnumerator SpawnChildren()
    {

        while (true)
        {
            switch (GameManager2._instance.levelNow)
            {
                case GameManager2.LevelNow.isLevel1:
                    GameManager2._instance.level1foodBase+= childrenAmount;
                    GameManager2._instance.level1foodNum+= childrenAmount;
                    break;
                case GameManager2.LevelNow.isLevel2:
                    GameManager2._instance.level2foodBase+= childrenAmount;
                    GameManager2._instance.level2foodNum+= childrenAmount;
                    break;
            }
            if (index== 0)
            {
                yield return new WaitForSeconds(2f);
            }
            else
            {
                yield return new WaitForSeconds(waitTime);
            }
            for (int i = 0; i < childrenAmount; i++)
            {
                float randomX = Random.Range(-maxDistance, maxDistance);
                float randomY = Random.Range(-maxDistance, maxDistance);
                float randomZ= Random.Range(-maxDistance, maxDistance);
                Instantiate(cakeChild,transform.localPosition+new Vector3(randomX,randomY,randomZ),Quaternion.identity);                        
            }
            index++;
        }
    }
}
