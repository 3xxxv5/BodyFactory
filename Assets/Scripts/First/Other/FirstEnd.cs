using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class FirstEnd : MonoBehaviour
{
    Transform tipBoard;
    bool toEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        tipBoard = transform.Find("tipBoard");
        tipBoard.gameObject.SetActive(false);
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
        Save._instance.SaveFairyCoinsAndTime(Level1UIManager._instance.fairyCoinsNum,Level1UIManager._instance.gameTime);
        Level1UIManager._instance.canWipeOut = true;
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("4_end");
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
