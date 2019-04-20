﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEnemy : BaseEnemy
{
    [SerializeField]
    float speed;


    public override void Move()
    {
        rg.velocity = new Vector2(speed * move, 0);
    }
}
