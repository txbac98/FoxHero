using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStockBullet : BaseEnemy
{

    [SerializeField]
    private float speed;

    Vector2 direction;
    // Use this for initialization
    void Start()
    {
        direction.x = FoxController.Instance.transform.position.x - transform.position.x;
        direction.y = FoxController.Instance.transform.position.y - transform.position.y;
    }

    public override void Move()
    {
        base.Move();
        Vector3 temp;
        temp = transform.position;
        temp.x += Time.deltaTime * direction.x * speed;
        temp.y += Time.deltaTime * direction.y * speed;
        transform.position = temp;
    }
    //// Update is called once per frame
    //void Update()
    //{
        
    //}
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
