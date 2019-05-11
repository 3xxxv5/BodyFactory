using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishTrigger : MonoBehaviour
{
    bool hasVanished = false;
    public TeachTower teachTower;
    bool startVanish = false;
    public  Renderer creeperRenderer;
    float growIndex = 1;
    float showSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        creeperRenderer.material.SetFloat("_dissolveAmount", growIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (startVanish)
        {
            growIndex = Mathf.Lerp(growIndex, 0, Time.deltaTime * showSpeed);
            creeperRenderer.material.SetFloat("_dissolveAmount", growIndex);
        }
        if (creeperRenderer.material.GetFloat("_dissolveAmount") > 0.99f)
        {
            startVanish = false;
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("Player"))
        {
            //路消失
            startVanish = true;
            //光柱消失
            teachTower.rayEmitter.lineRenderer.enabled = false;
            //花合上
            teachTower.anim.SetBool("close",true);

        }
    }
}
