using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : Item
{
    [SerializeField]
    int hpAdd;

    public override void EffectItem()
    {
        FoxHp.Instance.AddHp(hpAdd);
    }
}
