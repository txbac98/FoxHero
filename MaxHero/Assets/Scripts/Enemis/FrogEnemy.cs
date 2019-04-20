using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogEnemy : BaseEnemy
{
    [SerializeField]
    Animator myAnim;

    [SerializeField]
    Vector2 vec;

    [SerializeField]
    float timeDelayJump;

    float time;


    public override void Move()
    {
        base.Move();
        //if (FoxController.instance.transform.position)
        time += Time.deltaTime;
        if (time > timeDelayJump)
        {
            time = 0;
            rg.velocity = new Vector2(vec.x*move, vec.y);
            myAnim.SetTrigger("Jump");
        }  
    }
}
