using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GameDefine.TAG_PLAYER)
        {
            EffectItem();
            Destroy(this.gameObject);
        }
    }
    public virtual void EffectItem()
    {

    }
}
