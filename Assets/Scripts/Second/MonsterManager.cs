using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager _instance { get; private set; }
    //MonsterColor
    public Color once;
    public Color twice;
    public Color thirdTimes;
    public int dragonHp = 3;

    //掉落物buff
    [HideInInspector] public float buffDropSpeed = 0;
    float lowSpeedTime = 5f;

    private void Awake()
    {
        _instance = this;
    }
    public IEnumerator ChangeAllDropSpeed()
    {
        buffDropSpeed -= 1f;
        yield return new WaitForSeconds(lowSpeedTime);

        buffDropSpeed = 0;
    }

}
