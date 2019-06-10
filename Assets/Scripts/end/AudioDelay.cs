using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public struct _mySfx
{
    //public AudioSource sfx;
    public AudioClip sfx;
    

};
*/
public class AudioDelay : MonoBehaviour
{
    // public _mySfx[] MySfx;
    public float delayTime;
    // Start is called before the first frame update
    void Start()
    {
        //for (int n = 0; n < MySfx.Length; n++) {
            AudioSource Source;
            Source = GetComponent<AudioSource>();
           // Source.clip = MySfx[n].sfx;
            Source.PlayDelayed(delayTime);
        //}
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
