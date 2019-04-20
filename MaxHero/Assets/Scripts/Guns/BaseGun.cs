using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGun : MonoBehaviour
{
    public float dtSpawnBullet;
    float time;

    public BaseBullet bullet;
    bool isHoldMouse;

    public float angleLimit;

    Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {    
        LookMouse();
        CheckMouseButton();
    }
    void CheckMouseButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isHoldMouse = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isHoldMouse = false;
            time = 0;
        }
        if (isHoldMouse)
        {
            time += Time.deltaTime;
            if (time > dtSpawnBullet)
            {
                SpawnBullet();
            }
        }
    }
    void SpawnBullet()
    {
        time = 0;
        BaseBullet b = Instantiate<BaseBullet>(bullet);
        //if (FoxController.instance.facingRight)
            b.Init(transform.position, transform.rotation.eulerAngles.z, FoxController.Instance.facingRight);
        //else
        //{
        //    Debug.Log(transform.rotation.eulerAngles.z);
        //    b.Init(transform.position, transform.rotation.eulerAngles.z, FoxController.instance.facingRight);
        //}
    }
    void LookMouse()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // cách 1
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position) * Quaternion.Euler(0, 0, 90);

        //cách 2 (tối ưu hơn)
        float y = mousePos.y - transform.position.y;
        float x = Mathf.Abs(mousePos.x - transform.position.x);
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

        if (Mathf.Abs(angle) < angleLimit )
             transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }
}
