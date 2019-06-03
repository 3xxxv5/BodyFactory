using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Renderer bulletRenderer;
    float growIndex = 1;
    bool startVanish = false;
    private  float speed = 0.5f;
    private void Awake()
    {
        bulletRenderer = transform.GetComponent<Renderer>();
        bulletRenderer.material.SetFloat("_dissolveAmount", growIndex);
    }
    private void Update()
    {
        if (startVanish)
        {
            growIndex = Mathf.Lerp(growIndex, 0, speed*Time.deltaTime);
            bulletRenderer.material.SetFloat("_dissolveAmount", growIndex);
        }
        if (bulletRenderer.material.GetFloat("_dissolveAmount") < 0.01f)
        {
            startVanish = false;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("wall"))
        {
            GameManager2._instance.SpawnSpecialEffects("fire", transform.position, 5f);
            StartCoroutine(waitToVanish(1f));
        }
    }
    IEnumerator waitToVanish(float num)
    {
        yield return new WaitForSeconds(num);
        startVanish = true;       
    }
}
