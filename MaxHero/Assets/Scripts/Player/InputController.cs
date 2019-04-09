using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float dtSpawnBullet;
    float time;

    public BaseBullet bullet;
    bool isHoldMouse;

    public float angleLimit;

    Vector3 mousePos;

    private void Start()
    {
        Cursor.visible = false;
    }
    // Start is called before the first frame update
    void Update()
    {
        LookMouse();
        CheckMouseButton();
    }
    void CheckMouseButton()
    {
        if (Input.GetMouseButtonDown(0) )
        {
            isHoldMouse = true;
            GunManager.Instance.time = GunManager.Instance.timeDelay;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isHoldMouse = false;
            GunManager.Instance.time = 0;
        }
        if (isHoldMouse )
        {
             GunManager.Instance.Fire();
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
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

        if (Mathf.Abs(angle) < GunManager.Instance.angleLimit)
            //transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            GunManager.Instance.RotationGun(angle);
    }

}
