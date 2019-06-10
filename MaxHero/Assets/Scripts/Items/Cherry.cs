using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : Item
{
    [SerializeField]
    int hpAdd;

    public override void EffectItem()
    {
        FoxController.Instance.foxHp.AddHp(hpAdd);
    }
}
