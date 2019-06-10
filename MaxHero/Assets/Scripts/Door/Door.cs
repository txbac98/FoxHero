using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField]
    GameObject pressF, needKey;

    bool onF, onCollision;

    private void Update()
    {
        if (onCollision)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                onF = true;
            }
            if (onF)
            {
                if (FoxController.Instance.haveKey)
                {
                    onCollision = false;
                    GameManager.instance.NextScene();
                }
                else
                {
                    needKey.gameObject.SetActive(true);
                }
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag== GameDefine.TAG_PLAYER)
        {
            pressF.gameObject.SetActive(true);
            onCollision = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == GameDefine.TAG_PLAYER)
        {
            pressF.gameObject.SetActive(false);
            needKey.gameObject.SetActive(false);
            onCollision = false;
            onF = false;
        }
    }
}
