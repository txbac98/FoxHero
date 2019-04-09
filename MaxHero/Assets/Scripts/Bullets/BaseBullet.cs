using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public float speed;
    Vector2 vec;
    float damage;

    [SerializeField]
    Rigidbody2D rg;

    public virtual void Init( Vector3 pos, float rotateZ, bool faceRight)
    {
        transform.position = pos;
        
        vec.x = speed * Mathf.Cos(Mathf.Deg2Rad * rotateZ);
        vec.y = speed * Mathf.Sin(Mathf.Deg2Rad * rotateZ);

        //Neu quay phai
        if (faceRight)
        {
            vec.x = Mathf.Abs(vec.x);
            transform.rotation = Quaternion.Euler(0, 0, rotateZ);
        }
        else
        {
            // Quay trai
            transform.localScale *= new Vector2(-1, 1);
            transform.rotation = Quaternion.Euler(0, 0, -rotateZ);
            vec.x = -Mathf.Abs(vec.x);

            //Xét hướng bay
            if (rotateZ > 0)
            {
                vec.y = Mathf.Abs(vec.y);
            }
            else
            {
                vec.y = -Mathf.Abs(vec.y);
            }
        }


        rg.velocity = vec;
    }

}
