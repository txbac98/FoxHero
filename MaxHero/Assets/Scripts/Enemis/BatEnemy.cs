using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : BaseEnemy
{

    [SerializeField]
    GameObject left, right;

    [SerializeField]
    float speed;

    [SerializeField]
    float timeDelay;

    [SerializeField]
    GameObject batBullet;

    float time;

    bool onStart, inArea;


    public override void Move()
    {
        if (!onStart)
        {
            transform.localScale = new Vector2(-move * 3, 3);
            onStart = false;
            rg.velocity = new Vector2(speed * move, 0);
        }      
    }



    public void OnFire()
    {
        onStart = true;       
        StartCoroutine(IFire());
    }

    public void OffFire()
    {
        onStart = false;
    }

    IEnumerator IFire()
    {
        yield return new WaitForSeconds(1f);
        LookAtFox();
        rg.velocity = new Vector2(0, 0);
        GameObject b = Instantiate<GameObject>(batBullet);
        b.transform.position = transform.position;
        if (FoxController.Instance.transform.position.x <= transform.position.x)
        {
            b.GetComponent<BatBullet>().Spawn(-1);
        }
        else
        {
            b.GetComponent<BatBullet>().Spawn(1);
        }       
        if (onStart) StartCoroutine(IFire());
    }

    private void LookAtFox()
    {
        if (FoxController.Instance.transform.position.x <= transform.position.x)
        {
            transform.localScale = new Vector2(3, 3);
        }
        else
        {
            transform.localScale = new Vector2(-3, 3);
        }
        
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
