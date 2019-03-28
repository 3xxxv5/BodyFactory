using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class FirstEnd : MonoBehaviour
{
    Transform tipBoard;
    public Image fadeImage;
    bool toEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        tipBoard = transform.Find("tipBoard");
        tipBoard.gameObject.SetActive(false);
        fadeImage.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        if (tipBoard.gameObject.activeSelf && Input.GetKeyUp(KeyCode.E)&&(!toEnd))
        {
            StartCoroutine(toNext());
            toEnd = true;
        }
    }
    IEnumerator toNext()
    {
        fadeImage.DOFade(1, 1f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("2_official");
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("Player"))
        {
          tipBoard.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag.Equals("Player"))
        {
            tipBoard.gameObject.SetActive(false);
        }
    }
}
