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

    [SerializeField]
    public FoxHp foxHp;

    [SerializeField]
    GameObject key;

    public GunManager gunManager;

    [SerializeField]
    GameObject effectDeath;

    //Public variables
    [SerializeField]
    float maxSpeed, jumpHeight, speedClimb;

    //Private variables
    public bool facingRight;

    public bool haveKey;

    public bool dead;

    public bool haveGun;
    int numJump;

    float timeHurt;

    enum State
    {
        None=-1,
        Idle =0,
        Run = 1,
        Jump =2,
        Hurt=3,
        Climb =4,
        Fall=6,
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
        state = State.None;
        numJump = 0; //max 2
        facingRight = true;
        if (haveGun) gunManager.ChangeGun(1);
        //StartCoroutine(AutoCheckKey());
    }

    IEnumerator AutoCheckKey()
    {
        yield return new WaitForSeconds(0);
        if (state != State.Die)
        {
            CheckKey();
        }
        StartCoroutine(AutoCheckKey());
    }

    void FixedUpdate()
    {
        
    }
    private void Update()
    {
        if (state != State.Die)
        {
            CheckKey();
        }

        if (state == State.Hurt)
        {
            timeHurt += Time.deltaTime;
            if (timeHurt > 0.5f)
            {
                timeHurt = 0;
                state = State.None;
            }
        }
        else timeHurt = 0;
    }

    void ChangeState(State st)
    {
        switch (st)
        {
            case State.Idle:
                //numJump = 0;
                //myAnim.SetBool(GameDefine.ANIM_FOX_CLIMB, false);
                myAnim.SetTrigger(GameDefine.ANIM_FOX_IDLE);  
                myAnim.SetBool(GameDefine.ANIM_FOX_RUN, false);
                break;
            case State.Run:
                //myAnim.SetTrigger(GameDefine.ANIM_FOX_RUN);
                myAnim.SetBool(GameDefine.ANIM_FOX_RUN, true);
                //numJump = 0;
                break;
            case State.Jump:
                myBody.velocity = new Vector2(myBody.velocity.x, jumpHeight); //nhay
                myAnim.SetTrigger("Jump");
                myAnim.SetFloat("VecY", myBody.velocity.y);
                //audioSource.PlayOneShot(jumpClip);
                break;
            case State.Fall:

                break;
            case State.Climb:
                myAnim.enabled = true;
                myBody.gravityScale = 0;
                myBody.velocity = new Vector3(0, 0);
                myAnim.SetFloat(GameDefine.ANIM_FOX_VECY, 0);
                myAnim.SetBool(GameDefine.ANIM_FOX_CLIMB, true);
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
            //if (collision.gameObject.transform.position.y < transform.position.y)
            {
                numJump = 0;
                //myBody.velocity = new Vector3(myBody.velocity.x,0);
                if (state != State.Idle && state!=State.Run)
                {
                    ChangeState(State.Idle);
                }
            }          
        }
        if (collision.gameObject.tag == GameDefine.TAG_LADDER)
        {
            ChangeState(State.Climb);
        }
        if (collision.gameObject.tag == GameDefine.TAG_TOUCHWILL_DIE)
        {
            foxHp.OnMaxHit();
        }
        if (collision.gameObject.tag == GameDefine.TAG_NEXT_SCENE)
        {
            GameManager.instance.NextScene();
        }
    }
    public void OnHit(int dama)
    {
        if (state != State.Hurt)
        {
            ChangeState(State.Hurt);
            foxHp.OnHit(dama);
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
        CheckMove();
        CheckVecY();
        CheckJump();
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
        if (myBody.velocity.y < 0)
        {
            ChangeState(State.Fall);
        }
    }
    void CheckMove()
    {
        float move = 0;
        move = Input.GetAxis("Horizontal"); //theo Edit/project setting/ input
        //myAnim.SetFloat("Speed", Mathf.Abs(move));  //dieu kien, bien truyen vao (trong Animator)
        if (move != 0f)
            ChangeState(State.Run);
        else if (state==State.Run)
            ChangeState(State.Idle);
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


    //HP

    public void OnKey()
    {
        haveKey = true;
        key.SetActive(true);
    }
}
