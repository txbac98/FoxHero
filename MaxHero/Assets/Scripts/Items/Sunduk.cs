using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunduk : MonoBehaviour
{
    [SerializeField]
    GameObject gameObject;

    [SerializeField]
    Animator anim;

    bool open;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!open && collision.gameObject.transform.tag==GameDefine.TAG_PLAYER)
        {
            open = true;
            anim.SetTrigger("Open");
            GameObject b = Instantiate<GameObject>(gameObject);
            b.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f);
        }
    }
}
