using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour
{
    public static FoxController instance;
    //Object
    [SerializeField]
    Rigidbody2D myBody;

    [SerializeField]
    Animator myAnim;

    [SerializeField]
    FoxHp foxHp;

    [SerializeField]
    GameObject gun;


    //Public variables
    [SerializeField]
    float maxSpeed, jumpHeight, speedClimb;

    //Private variables
    public bool facingRight;
    int numJump;

    enum State
    {
        Idle =0,
        Run = 1,
        Jump =2,
        Crouch=3,
        Climb =4
    }

    State state;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        numJump = 0; //max 2
        facingRight = true;
    }

    void Update()
    {
        CheckKey();
        if (Input.GetKeyDown(KeyCode.C))
        {
            foxHp.AddDame(5);
        }
    }
    
    void ChangeState(State st)
    {
        //state = st;
        switch (st)
        {

            case State.Idle:
                myAnim.SetBool(GameDefine.FOX_CLIMB, false);
                myAnim.SetTrigger(GameDefine.FOX_IDLE);
                gun.SetActive(true);
                GunManager.Instance.canShoot = true;
                break;
            case State.Run:
                break;
            case State.Jump:
                myBody.velocity = new Vector2(myBody.velocity.x, jumpHeight); //nhay
                myAnim.SetTrigger("Jump");
                //audioSource.PlayOneShot(jumpClip);
                break;
            case State.Climb:
                myAnim.enabled = true;
                myBody.gravityScale = 0;
                gun.SetActive(false);
                myBody.velocity = new Vector3(0, 0);
                myAnim.SetFloat(GameDefine.FOX_VECY, 0);
                myAnim.SetBool(GameDefine.FOX_CLIMB,true);
                GunManager.Instance.canShoot = false;
                break;
            case State.Crouch:
                break;

        }
        state = st;
    }  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == GameDefine.TAG_GROUND)
        {
            numJump = 0;
            myBody.velocity = new Vector3(myBody.velocity.x,0);
        }
        if (collision.gameObject.tag == GameDefine.TAG_LADDER)
        {
            ChangeState(State.Climb);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == GameDefine.TAG_LADDER)
        {
                ChangeState(State.Climb);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == GameDefine.TAG_LADDER)
        {
             myBody.gravityScale = 1;
             ChangeState(State.Idle);
        }
    }

    //Input
    void CheckKey()
    {
        CheckCrouch();
        if (state != State.Climb)
        {
            CheckVecY();
        }
        if (state != State.Crouch)
        {
            CheckClimb();
            CheckMove();
            if (state != State.Climb)
            {
                CheckJump();       
            }        
        }     
    }
    void CheckClimb()
    {
        if (state == State.Climb)
        {
            myBody.gravityScale = 0;
            if (Input.GetKey(KeyCode.W))
            {
                myAnim.enabled = true;
                transform.position += new Vector3(0, speedClimb * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                myAnim.enabled = true;
                transform.position += new Vector3(0, -speedClimb * Time.deltaTime);
            }
            //else myAnim.enabled = false;
        }
    }
    void CheckJump()
    {
        //Nhay
        if (Input.GetKeyDown(KeyCode.W))
        {
           Jump();
        }    
    }
    void Jump()
    {
        if (numJump < 2)
        {
            numJump++;
            ChangeState(State.Jump);
            
        }
    }

    
    void CheckVecY()
    {
        myAnim.SetFloat("VecY", myBody.velocity.y);
        //if (myBody.velocity.y > 0)
        //{
        //    myAnim.SetFlo("VecY", 1);
        //}
        //else if (myBody.velocity.y < 0)
        //{
        //    myAnim.SetInteger("VecY", -1);
        //}
        //else myAnim.SetInteger("VecY", 0);
    }
    void CheckCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            myBody.velocity = new Vector3(0, 0);
            myAnim.SetTrigger(GameDefine.FOX_CROUCH);
            state = State.Crouch;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            myAnim.SetTrigger(GameDefine.FOX_IDLE);
            state = State.Idle;
        }
    }
    void CheckMove()
    {
        float move = Input.GetAxis("Horizontal"); //theo Edit/project setting/ input
        myAnim.SetFloat("Speed", Mathf.Abs(move));  //dieu kien, bien truyen vao (trong Animator)
        myBody.velocity = new Vector2(move * maxSpeed, myBody.velocity.y); //di theo vector 

        if (move > 0 && !facingRight) //qua phai, mat trai
        {
            flip();
        }
        else if (move < 0 && facingRight) //qua trai, mat phai
        {
            flip();
        }
    }
    void  flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale; //Lay scale
        theScale.x *= -1;   //doi chieu
        transform.localScale = theScale;    //gan ve
    }
}
