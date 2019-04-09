using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKGun : Gun
{
    public AKGun()
    { }
    public AKGun(int id)
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
