using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : Item
{
    public override void EffectItem()
    {
        FoxController.Instance.OnKey();
    }
}
