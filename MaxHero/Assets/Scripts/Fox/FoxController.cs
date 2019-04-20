using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour
{
    public static FoxController Instance;
    //Object
    
    public Rigidbody2D myBody;

    [SerializeField]
    Animator myAnim;

    //[SerializeField]
    //FoxHp foxHp;

    [SerializeField]
    GameObject gun;

    [SerializeField]
    GameObject effectDeath;

    //Public variables
    [SerializeField]
    float maxSpeed, jumpHeight, speedClimb;

    //Private variables
    public bool facingRight;
    public bool dead;
    int numJump;

    enum State
    {
        Idle =0,
        Run = 1,
        Jump =2,
        Hurt=3,
        Climb =4,
        Die=5
    }

    State state;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        numJump = 0; //max 2
        facingRight = true;
    }

    void Update()
    {
        if (state!=State.Die)
        {
            CheckKey();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            FoxHp.Instance.AddHp(100);
            ChangeState(State.Die);
        }

    }
    
    void ChangeState(State st)
    {
        //state = st;
        switch (st)
        {

            case State.Idle:
                myAnim.SetBool(GameDefine.ANIM_FOX_CLIMB, false);
                myAnim.SetTrigger(GameDefine.ANIM_FOX_IDLE);
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
                myAnim.SetFloat(GameDefine.ANIM_FOX_VECY, 0);
                myAnim.SetBool(GameDefine.ANIM_FOX_CLIMB, true);
                GunManager.Instance.canShoot = false;
                break;
            case State.Hurt:
                myAnim.SetTrigger(GameDefine.ANIM_FOX_HURT);
                break;
            case State.Die:
                GameObject b = Instantiate<GameObject>(effectDeath);
                b.transform.position = transform.position;
                //Destroy(this.gameObject);
                this.gameObject.SetActive(false);
                dead = true;
                break;

        }
        state = st;
    }  


    //Death
    public void Death()
    {
        ChangeState(State.Die);
    }

    //Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == GameDefine.TAG_GROUND)
        {
            numJump = 0;
            myBody.velocity = new Vector3(myBody.velocity.x,0);
            if (myBody.velocity.x==0 && state != State.Hurt)
            {
                ChangeState(State.Idle);
            }
        }
        if (collision.gameObject.tag == GameDefine.TAG_LADDER)
        {
            ChangeState(State.Climb);
        }
        if (collision.gameObject.tag == GameDefine.TAG_ENEMY)
        {
            ChangeState(State.Hurt);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GameDefine.TAG_ENEMY)
        {
            ChangeState(State.Hurt);
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


    //rigidbody

    public void OnGravityScale()
    {
        myBody.gravityScale = 1;
    }
    public void OffGravityScale()
    {
        myBody.gravityScale = 0;
    }

    //Input
    void CheckKey()
    {
        if (state != State.Climb)
        {
            CheckVecY();
        }
        CheckClimb();
        CheckMove();
        if (state != State.Climb)
        {
            CheckJump();       
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
