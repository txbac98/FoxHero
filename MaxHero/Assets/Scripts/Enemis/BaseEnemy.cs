using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField]
    protected Hp hp;

    [SerializeField]
    protected SpriteRenderer sprite;

    public Rigidbody2D rg;

    [SerializeField]
    GameObject deathEffect;

    [SerializeField]
    public int dama;

    bool beingAttacked;

    float timeColor;

    public int move;



    // Start is called before the first frame update
    void Start()
    {
        timeColor = 0;
        beingAttacked = false;
    }

    public virtual void Update()
    {
        CheckBeingAttacked();
        Move();
    }

    public virtual void Move()
    {

    }
    public virtual void Flip()
    {
        rg.velocity = new Vector2(-rg.velocity.x,rg.velocity.y);
        move *= -1;
        transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y);
    }
    void CheckBeingAttacked()
    {
        if (beingAttacked)
        {
            timeColor += Time.deltaTime;
            if (timeColor > 0.1f)
            {
                timeColor = 0;
                NotAttacked();
            }
        }
    }

    protected virtual void Death()
    {
        if (deathEffect != null)
        {
            GameObject d = Instantiate<GameObject>(deathEffect);
            d.transform.position = transform.position;
        }
        Destroy(this.gameObject);
    }

    private void OnHit(int dama)
    {
        if (hp)
        {
            hp.AddDame(dama);
            if (hp.HP <= 0) Death();
            if (hp.bar)
                hp.bar.SetActive(true);
        }
        
        BeingAttacked();
        
    }
    virtual public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == GameDefine.TAG_PLAYER_BULLET)
        {
                OnHit(collision.gameObject.GetComponent<BaseBullet>().damage);
        }
        
        if (collision.gameObject.tag == GameDefine.TAG_PLAYER_BULLET)
        {
            OnHit(collision.gameObject.GetComponent<BaseBullet>().damage);
        }

        if (collision.gameObject.tag == GameDefine.TAG_CHANGE_DIRECTION_ENEMY)
        {
            Flip();
        }
        if (collision.gameObject.tag == GameDefine.TAG_PLAYER)
        {
            FoxController.Instance.OnHit(dama);
        }
    }
    virtual public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == GameDefine.TAG_PLAYER && gameObject.tag !=GameDefine.TAG_BOX)
        {
            FoxController.Instance.OnHit(dama);
        }
    }
    private void BeingAttacked()
    {
        if (sprite)
        {
            sprite.color = Color.red;
        }        
        beingAttacked = true;
    }
    private void NotAttacked()
    {
        if (sprite)
        {
            sprite.color = Color.white;
        }       
       beingAttacked = false;
    }
}
