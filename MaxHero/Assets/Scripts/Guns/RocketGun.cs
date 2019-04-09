using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketGun : Gun
{
    public RocketGun()
    { }
    public RocketGun(int id)
    {
        this.id = id;
    }
    public override Sprite GetSprite()
    {
        return base.GetSprite();
    }
    public override void Init()
    {
        base.Init();
    }
    public override void Fire(Vector3 pos)
    {
        base.Fire(pos);
    }
}
