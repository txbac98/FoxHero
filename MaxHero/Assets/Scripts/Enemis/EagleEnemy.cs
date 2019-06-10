using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleEnemy : BaseEnemy
{
    [SerializeField]
    AreaEagle areaEagle;

    public bool beingAttack;

    [SerializeField]
    float speed;

    [SerializeField]
    float timeDelayAttack;

    
    float time;

    Vector3 foxPos;


    void Update()
    {
        LookAtFox();
     
        if (areaEagle.inArea)
        {
            if (beingAttack)
            {
                transform.position = Vector3.MoveTowards(transform.position, foxPos, 4 * speed * Time.deltaTime); //Tan cong (3,3)
            }
            else transform.position += new Vector3(0.5f * speed * move * Time.deltaTime, speed * Time.deltaTime);  //Bay len khi danh xong (0,5,1)


            if (time == 0)
            {
                Attack();
            }

            time += Time.deltaTime;
            if (time > timeDelayAttack)
            {
                time = 0.01f;
                Attack();
            }
            if (transform.position == foxPos)
            {
                beingAttack = false;
            }
        }

        base.Update();

    }

    private void Attack()
    {
        beingAttack = true;
        GetFoxPos();
    }
    private void GetFoxPos()
    {
        foxPos = FoxController.Instance.transform.position;
    }


    private void LookAtFox()
    {
        if (FoxController.Instance.transform.position.x <= transform.position.x)
        {
            move = 1;
        }
        else
        {
            move = -1;          
        }
        transform.localScale = new Vector2(move*3, 3);
    }


    public override void Flip()
    {
        //Để mất va chạm với change direction enemy
    }
}
