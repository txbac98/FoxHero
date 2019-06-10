using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItem : Item
{
    public override void EffectItem()
    {
        FoxController.Instance.haveGun = true;
        FoxController.Instance.gunManager.ChangeGun(1);
    }
}
