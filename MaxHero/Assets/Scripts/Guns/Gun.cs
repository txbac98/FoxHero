using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun
{
    public int id;
    public float angle;
    float time;
    float timeDelay;

    public Gun()
    { }
    public Gun(int id, float tDelay)
    {
        this.id = id;
        this.timeDelay = tDelay;
    }
    public virtual void Init()
    {

    }
        
    public virtual Sprite GetSprite()
    {
        return null;
    }
    public virtual void RotationGun(float angle)
    {
        this.angle = angle;
    }
    public virtual void Fire(Vector3 pos)
    {
        BaseBullet b = BulletManager.Instance.Spawn(this.id);
        if(b != null)
        {
            b.Init(pos, angle, FoxController.instance.facingRight);
        }
    }
}
