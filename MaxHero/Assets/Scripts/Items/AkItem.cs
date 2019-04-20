using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkItem : Item
{
    public override void EffectItem()
    {
        //FoxHp.Instance.AddHp(hpAdd);
        GunManager.Instance.ChangeGun(0);
    }
}
