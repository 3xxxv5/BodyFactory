using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMeshCollider : MonoBehaviour
{
    public Rigidbody[] doorChips;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < doorChips.Length; i++)
        {
            doorChips[i].isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("dragon"))
        {
              for (int i = 0; i < doorChips.Length; i++)
            {
                doorChips[i].isKinematic = false;
            }
        }
    }
}
