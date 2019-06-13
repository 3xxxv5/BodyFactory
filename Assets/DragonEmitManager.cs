using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEmitManager : MonoBehaviour
{
    public static DragonEmitManager _instance { get; private set; }
    public MyQueue[] ballWave;
    public float waveRate = 5;

    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {

    }

    void Update()
    {

    }
    public IEnumerator SpawnLightningBall()
    {
        yield return new WaitForSeconds(3f);
        for (int v = 0; v < ballWave.Length; v++)
        {
            for (int i = 0; i < ballWave[v].count; i++)
            {
                AudioManager._instance.PlayEffect("");
                Instantiate(ballWave[v].foodPrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(ballWave[v].rate);  
            }
            if (v != ballWave.Length - 1)
            {
                yield return new WaitForSeconds(waveRate);
            }
        }
    }
    void SpawnFood(GameObject prefab)
    {

    }
}
