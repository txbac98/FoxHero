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

    // Update is called once per frame
    void Update()
    {
        Vector3 temp;
        temp = transform.position;
        temp.x += Time.deltaTime * direction.x * speed;
        temp.y += Time.deltaTime * direction.y * speed;
        transform.position = temp;



    }
}
