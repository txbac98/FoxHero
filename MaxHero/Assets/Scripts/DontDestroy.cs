using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    static DontDestroy _instance;
    private void Awake()
    {
        //GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        //if (objs.Length > 1)
        //{
        //    Destroy(this.gameObject);
        //}

        //DontDestroyOnLoad(this.gameObject);
        //if we don't have an [_instance] set yet
        if (!_instance)
            _instance = this;
        else
            Destroy(this.gameObject);


        DontDestroyOnLoad(this.gameObject);
    }
}
