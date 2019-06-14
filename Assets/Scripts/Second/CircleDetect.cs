using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDetect : MonoBehaviour
{

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("wuzei"))
        {
            if (GameManager2._instance.levelNow == GameManager2.LevelNow.isLevel3)
            {
                StartCoroutine(DragonEmitManager._instance.SpawnCircleBall());
            }
        }
    }
}
