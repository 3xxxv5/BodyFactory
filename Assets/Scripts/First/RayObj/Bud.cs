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
    Renderer creeperRenderer;
    float growIndex = 0;
    bool startShow = false;
    public float showSpeed = 1.0f;
    public Color creeperColor;

    private void Awake()
    {
        leaves = GetComponentsInChildren<TagLeave>();        
        needRayNum = leaves.Length;
        nowRayNum = 0;
        blackBud = Resources.Load<Material>(MainContainer.materialFolder+ "myLampNor");
        glowBud = Resources.Load<Material>(MainContainer.materialFolder + "myLampEmi");
        for (int i = 0; i < needRayNum; i++)
        {
            leaves[i].gameObject.GetComponent<MeshRenderer>().material = blackBud;
        }
        creeper = transform.Find("creeper");
        creeper.gameObject.AddComponent<HighlightableObject>();
        creeperRenderer = creeper.GetComponent<Renderer>();
        creeperRenderer.material = Resources.Load<Material>(MainContainer.materialFolder + "myDissolve");
        creeperRenderer.material.SetFloat("_dissolveAmount", growIndex);
        creeper.gameObject.SetActive(false);

    }	

	void Update () {
        LightAndDarkLeaves();        
        GrowCreeper();
        if (startShow)
        {
            growIndex = Mathf.Lerp(growIndex, 1, Time.deltaTime * showSpeed);
            creeperRenderer.material.SetFloat("_dissolveAmount", growIndex);
        }
        if (creeperRenderer.material.GetFloat("_dissolveAmount") > 0.99f)
        {
            startShow = false;
        }
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
        if (nowRayNum >= needRayNum && ! hasGrowrn)
        {
            AudioManager._instance.PlayEffect("creeper");
            creeper.gameObject.SetActive(true);
            startShow = true;
            //StartCoroutine(HighlightCreeper());
            hasGrowrn = true;           
        }       
    }

    IEnumerator HighlightCreeper()
    {
        HighlightableObject obj = creeper.gameObject.GetComponent<HighlightableObject>();        
        obj.ConstantOn(creeperColor);
        yield return new WaitForSeconds(5);
        obj.ConstantOff();
    }
}
