using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStock : BaseEnemy
{
    [SerializeField]
    GunStockBullet bullet;

    [SerializeField]
    float timeDelay;

    float time;
    bool onSpawn;

    public override void Move()
    {
        base.Move();
        if (onSpawn)
        {
            time += Time.deltaTime;
            if (time > timeDelay)
            {
                time = 0;
                GunStockBullet b = Instantiate<GunStockBullet>(bullet);
                b.transform.position = transform.position;
            }
        }       
    }

    public void OnSpawn()
    {
        onSpawn = true;
    }

    public void OffSpawn()
    {
        onSpawn = false;
    }
}
