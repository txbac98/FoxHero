using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEnemy : BaseEnemy
{

    [SerializeField]
    GameObject left, right;

    [SerializeField]
    AreaMouse area;

    [SerializeField]
    float speed;

    [SerializeField]
    float timeDelay;



    float time;

    bool onStart, inArea;

    Vector3 pos0;


    public override void Move()
    {
        //CheckInArea();
        if (area.inArea)
        {
            if (!onStart) // Đợi
            {
                LookAtFox();
                time += Time.deltaTime;
                if (time > timeDelay)
                {
                    time = 0;
                    onStart = true;
                    pos0 = FoxController.Instance.transform.position;
                    pos0.y = transform.position.y;
                }
            }
            else
            { 
                transform.position = Vector3.MoveTowards(transform.position, pos0, 3 *speed * Time.deltaTime);
                if (transform.position.x == pos0.x)
                {
                    onStart = false;
                }
            }
        }
        else
        {
            transform.localScale = new Vector2(-move * 3, 3);
            onStart = false;
            rg.velocity = new Vector2(speed * move, 0);    
        }
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
        transform.localScale = new Vector2(move * 3, 3);
    }

    void CheckInArea()
    {
        if (FoxController.Instance.transform.position.x > left.transform.position.x && FoxController.Instance.transform.position.x < right.transform.position.x)
        {
            inArea = true;
        }
        else inArea = false;
    }
}
