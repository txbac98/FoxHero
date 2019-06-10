using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    GunManager gunManager;

    //public float dtSpawnBullet;
    float time;


    bool isHoldMouse;

    //public float angleLimit;

    Vector3 mousePos;

    private void Start()
    {
        Cursor.visible = false;
    }
    // Start is called before the first frame update
    void Update()
    {
        if (gunManager)
        {
            LookMouse();
            CheckMouseButton();
        }      
    }
    void CheckMouseButton()
    {
        if (Input.GetMouseButtonDown(0) )
        {
            isHoldMouse = true;
            gunManager.time = gunManager.timeDelay;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isHoldMouse = false;
            gunManager.time = 0;
        }
        if (isHoldMouse )
        {
             gunManager.Fire();
        }
    }
    void LookMouse()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // cách 1
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position) * Quaternion.Euler(0, 0, 90);

        //cách 2 (tối ưu hơn)
        float y = mousePos.y - transform.position.y;
        float x = Mathf.Abs(mousePos.x - transform.position.x);
        float angle =Mathf.Atan2(y, x) * Mathf.Rad2Deg;

        if ((angle > -10) && (angle < gunManager.angleLimit))
            //transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            gunManager.RotationGun(angle);
    }

}
