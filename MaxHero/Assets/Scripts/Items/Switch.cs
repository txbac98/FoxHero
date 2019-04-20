using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    [SerializeField]
    Wood wood;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GameDefine.TAG_PLAYER)
        {
            wood.onMove = true;
            anim.SetBool("OnSwitch", true);
        }
        //if 
    }
}
