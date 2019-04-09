using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance;
    public List<BaseBullet> bullets;
    private void Awake()
    {
        BulletManager.Instance = this;
    }
    public BaseBullet Spawn(int id)
    {
        
        BaseBullet b = Instantiate(bullets[id]) as BaseBullet;
        //if (b != null)
        //{
        //    b.transform.position = pos;
        //    //b.Init(pos)
        //}
        return b;
    }

}
