using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class Bud : MonoBehaviour {
    public int nowRayNum;
    [HideInInspector]
    public int needRayNum;//需要点亮几片叶子，才会长出藤蔓
    public  bool hasGrowrn = false;
    TagLeave[] leaves;
    Material blackBud;
    Material glowBud;
    Transform creeper;
    Animator creeperAnim;

    private void Awake()
    {
        leaves = GetComponentsInChildren<TagLeave>();        
        needRayNum = leaves.Length;
        nowRayNum = 0;
        blackBud = Resources.Load<Material>(MainContainer.materialFolder+"blackBud");
        glowBud = Resources.Load<Material>(MainContainer.materialFolder + "glowBud");
        for (int i = 0; i < needRayNum; i++)
        {
            leaves[i].gameObject.GetComponent<MeshRenderer>().material = blackBud;
        }
        creeper = transform.Find("creeper");
        creeper.gameObject.AddComponent<HighlightableObject>();
        creeper.gameObject.SetActive(false);        
    }	

	void Update () {
        if (hasGrowrn) return;        
        LightAndDarkLeaves();        
        GrowCreeper();            
    }
    void LightAndDarkLeaves()
    {
        nowRayNum = Mathf.Clamp(nowRayNum, 0, needRayNum);
        for (int i = 0; i < nowRayNum; i++)
        {
            leaves[i].gameObject.GetComponent<MeshRenderer>().material = glowBud;
        }
        for (int i = nowRayNum; i < needRayNum; i++)
        {
            leaves[i].gameObject.GetComponent<MeshRenderer>().material = blackBud;
        }
    }
    void GrowCreeper()
    {
        if (nowRayNum >= needRayNum)
        {
            AudioManager._instance.PlayEffect("creeper");
            creeper.gameObject.SetActive(true);
            hasGrowrn = true;
            StartCoroutine(HighlightCreeper());
        }
    }

    IEnumerator HighlightCreeper()
    {
        HighlightableObject obj = creeper.gameObject.GetComponent<HighlightableObject>();
        obj.ConstantOn(Color.green);
        yield return new WaitForSeconds(5);
        obj.ConstantOff();
    }
}
