using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("monster"))
        {
            audioSource.PlayOneShot(GameManager2._instance.gunClip, GameManager2._instance.Volume / 10);//音效
            GameObject fire= Instantiate(GameManager2._instance.fire,transform.position,Quaternion.identity); Destroy(fire, 0.5f);//爆炸特效
            if (col.gameObject.tag.Equals("chocolateFrog")) { StartCoroutine(GameManager2._instance.ChangeAllDropSpeed()); }//巧克力蛙buff

            //同归于尽
            Destroy(col.gameObject);
            Destroy(gameObject);            
        }
    }
}
