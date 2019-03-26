using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    public static int level1Hp = 5;
    // Start is called before the first frame update
    void Start()
    {
        Level1_Init();
    }

    // Update is called once per frame
    void Update()
    {
        CheckLevel1Over();
    }

    void CheckLevel1Over()
    {
        if (level1Hp <= 0)
        {
            //输了
            //AudioManager._instance.PlayEffect("");
            //Level1Restart();
        }
    }
    void Level1Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Level1_Init()
    {
        //主角位置

        //楼层开启

        //Wave数组选用
    }
}
