using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
    public  float dropSpeed=2f;
    public GameObject chips;
    protected float trueSpeed;
    public  int lifeBase = 2;
    [HideInInspector]public  int life = 0;
    HighlightableObject ho;

	void Awake () {
        if (chips == null) chips = Resources.Load<GameObject>("Prefabs/m_donutChips");
        life = lifeBase;
        ho = gameObject.AddComponent<HighlightableObject>();
    }
	

	void Update () {
          Move();
          ChangeColor();
    }
    void ChangeColor()
    {
        switch (life)
        {
            case 3:
                ho.ConstantOn(Color.red);
                break;
            case 2:
                ho.ConstantOn(Color.blue);
                break;
            case 1:
                ho.ConstantOn(Color.green);
                break;
        }
    }
    protected void Move()
    {
            trueSpeed = dropSpeed + GameManager2._instance.buffDropSpeed;
            Mathf.Clamp(trueSpeed, 1f, 10);
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, -1, 0), Time.deltaTime * trueSpeed);
    }
   public  void ReduceLife(int num)
    {
        life -= num;        
    }
    protected void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("bigBullet"))
        {          
            Destroy(gameObject);
        }
    }
}
