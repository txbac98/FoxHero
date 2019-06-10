using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBullet : BaseEnemy
{

    [SerializeField]
    private float speed;

    public void Spawn(int _move)
    {
        move = _move;
        if (move > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
        }
    }

    void Update()
    {
        transform.position += new Vector3(move * speed * Time.deltaTime, 0);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GameDefine.TAG_BOX_CAMERA)
        {
            Destroy(this.gameObject);
        }
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag == GameDefine.TAG_PLAYER || collision.gameObject.tag == GameDefine.TAG_PLAYER_BULLET)
        {
            Destroy(gameObject);
        }
    }
}
