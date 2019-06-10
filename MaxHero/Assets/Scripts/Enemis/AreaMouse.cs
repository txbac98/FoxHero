using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaMouse : MonoBehaviour
{
    public bool inArea;

    [SerializeField]
    GameObject mouse;

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == GameDefine.TAG_PLAYER)
        {
            inArea = true;
            if (mouse)
                mouse.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == GameDefine.TAG_PLAYER)
        {
            inArea = false;
        }
    }
}
