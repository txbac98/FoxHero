using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Areabat : MonoBehaviour
{

    [SerializeField]
    BatEnemy bat;


    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == GameDefine.TAG_PLAYER)
        {
            if (bat)
                bat.OnFire();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == GameDefine.TAG_PLAYER)
        {
            if (bat)
                bat.OffFire();
        }
    }
}
