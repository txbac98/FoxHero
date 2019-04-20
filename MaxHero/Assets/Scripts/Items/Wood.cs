using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    public bool onMove;

    [SerializeField]
    Rigidbody2D rg;

    [SerializeField]
    Vector2 vec;

    Vector3 dtPos, pos;

    bool onFox;

    void Update()
    {   
        if (onMove)
        {
            pos = new Vector3(vec.x * Time.deltaTime, vec.y * Time.deltaTime);
            transform.position += pos;
            if (onFox)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    onFox = false;
                    FoxController.Instance.OnGravityScale();
                }
                else
                {
                    FoxController.Instance.transform.position = new Vector3(FoxController.Instance.transform.position.x, transform.position.y +dtPos.y);
                }             
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GameDefine.TAG_CHANGE_DIRECTION_WOOD)
        {
            vec *= new Vector2(-1,-1);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == GameDefine.TAG_PLAYER)
        {
            onFox = true;
            FoxController.Instance.OffGravityScale();
            dtPos = FoxController.Instance.transform.position - transform.position;
          
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == GameDefine.TAG_PLAYER)
        {
            onFox = false;
            FoxController.Instance.OnGravityScale();
        }
    }

}
