using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public static Dragon _instance { get; private set; }
    public GameObject chips;
    public int lifeBase = 3;
    [HideInInspector] public int life = 0;

    private void Awake()
    {
        _instance = this;
        life = lifeBase;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ReduceLife(int num)
    {
        life -= num;
    }
    public void BeAttacked(Vector3 pos)
    {
        print("击中龙了！"); AudioManager._instance.PlayEffect("gun");  
        ReduceLife(1);
        if (life < 1)
        {
            //龙爆炸的大特效
            GameManager2._instance.SpawnSpecialEffects("dragonExplode", transform.position, 5f);
            Destroy(gameObject,3f);
        }
    }
}
