using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEagle : MonoBehaviour
{
    public bool inArea;



    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == GameDefine.TAG_PLAYER)
        {
            inArea = true;
        }
    }
}
