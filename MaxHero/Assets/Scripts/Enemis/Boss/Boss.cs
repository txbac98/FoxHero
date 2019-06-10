using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : BaseEnemy
{
    [SerializeField]
    Animator animator;


    [SerializeField]
    GameObject[] listPos;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    GameObject[] listEnemy;

    [SerializeField]
    float speed;

    float time;

    float timeAttack;

    float timeAliveAttack, timeDelayAttack;

    float timeDelayMove;

    int rand;

    int index;

    int indexAttack;

    bool doneAttack;

    // Start is called before the first frame update
    void Start()
    {
        rand = Random.Range(10, 30);
        timeDelayMove = (float)(rand) / 10;

        rand = Random.Range(20, 30);
        timeDelayAttack = (float)(rand) / 10;

        index = Random.Range(0, listPos.Length);
        doneAttack = true;
    }

    public override void Move()
    {
        base.Move();
        AutoMove_Attack();          
    }

    void AutoMove_Attack()
    {
        if (doneAttack && animator.GetBool(GameDefine.ANIM_BOSS_IDLE)) //Đang đứng yên mới di chuyển
        {
            time += Time.deltaTime;
            if (time > timeDelayMove)
            {
                doneAttack = false;
                time = 0;
                rand = Random.Range(10, 30);
                timeDelayMove = (float)(rand) / 10;
                index = Random.Range(0, listPos.Length);
                Debug.Log(index);
                
            }
            else _Move();
        }
        else
        {
            time += Time.deltaTime;
            if (time > timeDelayAttack)
            {
                doneAttack = true;
                time = 0;
                rand = Random.Range(20, 30);
                timeDelayAttack = (float)(rand) / 10;
                if (hp.HP < 50)
                    rand = Random.Range(1, 3);
                else rand = Random.Range(0, 2);
                LookAtFox();
                animator.SetBool(GameDefine.ANIM_BOSS_IDLE, false);
                if (rand == 1) animator.SetTrigger(GameDefine.ANIM_BOSS_DASS);
                if (rand == 0) animator.SetTrigger(GameDefine.ANIM_BOSS_FIRE);
                if (rand == 2) animator.SetTrigger(GameDefine.ANIM_BOSS_ATTACK);
            }
        }
    }


    void _Move()
    {
        LookAtFox();
        transform.position = Vector3.MoveTowards(transform.position, listPos[index].transform.position, speed * Time.deltaTime); 
    }


    private void LookAtFox()
    {
        if (FoxController.Instance.transform.position.x <= transform.position.x)
        {
            transform.localScale = new Vector2(4, 4);
        }
        else
        {
            transform.localScale = new Vector2(-4, 4);
        }
        
    }

    protected override void Death()
    {
        animator.SetTrigger(GameDefine.ANIM_BOSS_DIE);
    }

    public void Fire()
    {
        rand = Random.Range(0, listEnemy.Length);
        GameObject e = Instantiate<GameObject>(listEnemy[rand]);
        e.transform.position = listEnemy[rand].transform.position;
        e.SetActive(true);
    }

    public void Attack()
    {
        GameObject b = Instantiate<GameObject>(bullet);
        b.transform.position = transform.position;
        if (transform.localScale.x > 0)
        {
            b.GetComponent<BatBullet>().Spawn(-1);
        }
        else b.GetComponent<BatBullet>().Spawn(1);
    }
}
