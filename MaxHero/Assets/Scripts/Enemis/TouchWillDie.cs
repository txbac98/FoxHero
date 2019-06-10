﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchWillDie : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GameDefine.TAG_PLAYER)
        {
            FoxController.Instance.OnHit(100);
        }
    }
}
