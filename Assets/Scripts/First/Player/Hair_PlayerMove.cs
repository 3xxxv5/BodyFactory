using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hair_PlayerMove : MonoBehaviour
{
    public  Animator anim;
    Rigidbody rb;
    public float forwardSpeed = 7.0f;
    public float backwardSpeed =2.0f;
    public float climbSpeed = 2.0f;
    public float jumpSpeed = 2.0f;
    FreeCamera freeCamera;
    public  bool canMove = true;
    public bool canClimb = false;
    public bool isClimbing = false;
    public bool isGrounded = true;
    public bool animPaused = false;
    

    void Start()
    {
        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        freeCamera = Camera.main.GetComponent<FreeCamera>();
        
    }


    void FixedUpdate()
    {
        ClimbManager();
       
        AnimPauseManager();
    }

    void AnimPauseManager()
    {
        if (animPaused)
        {
            transform.GetComponent<Rigidbody>().isKinematic = true;
            transform.GetComponent<Rigidbody>().useGravity = false;
        }
        else
        {
            transform.GetComponent<Rigidbody>().isKinematic = false;
            transform.GetComponent<Rigidbody>().useGravity = true;
        }
    }
    void MoveManager()
{
        if (!canMove || anim.GetBool("Climb") || animPaused) return;
        if (Input.GetButtonUp("Jump"))
        {
            if (isGrounded)//只有在地面上才能跳
            {
                rb.velocity += new Vector3(0, jumpSpeed, 0);
                rb.AddForce(Vector3.up*jumpSpeed);
            }
        }
        print("velocityY: " + rb.velocity.y);

        /*
         * if(Input.GetButton("Jump")){
         *    anim.SetBool("is_in_air",true);
         *    moveDirection.y=jumpSpeed;
         * }else{
         * anim.SetBool("is_in_air",false);
         * }
         
         */
        float v;
        if (Input.GetKey(KeyCode.W))
        {
            v = 1;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            v = -1;
        }
        else
        {
            v = 0;
        }
        float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        //动画
        anim.SetFloat("Speed", v);//speed>0.1前进，<-0.1后退       
        anim.SetFloat("Direction", h);
       
        //前进速度
        Vector3 vertical_velocity = transform.forward * v * forwardSpeed;
        Vector3 horizontal_velocity = transform.right * h * backwardSpeed;
        Vector3 moveVelocity = vertical_velocity + horizontal_velocity;
        Vector3 velocity = rb.velocity;
        rb.velocity = new Vector3(moveVelocity.x,rb.velocity.y,moveVelocity.z);
}

    void ClimbManager()
    {
            if (canClimb && Input.GetKeyUp(KeyCode.E))//在爬行区域内，且按下爬行建
            {
                anim.SetBool("Climb", true);
                isClimbing = true;
                AudioManager._instance.PlayEffect("climb");
        }
            else
            {
                MoveManager();
                anim.SetBool("Climb", false);
            }
            if (isClimbing)//爬行中，设置向上的速度
            {
             
                rb.velocity = Vector3.up * climbSpeed;
            }
            else
            {                
                anim.SetBool("Climb", false);
            }                      
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("ground")) isGrounded = true;
    }
    private void OnCollisionExit(Collision col)
    {
        if(col.gameObject.tag.Equals("ground")) isGrounded = false;
    }
}
