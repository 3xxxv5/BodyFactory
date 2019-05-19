using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ToLevel2 : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(toNext());
    }
    IEnumerator toNext()
    {
        yield return new WaitForSeconds((float)videoPlayer.length);
        SceneManager.LoadScene("2_official");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
