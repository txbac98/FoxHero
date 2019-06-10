using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    Transform posTranLeft, posTranRight;

    Transform target; // nv theo doi
    public float smoothing; //muot ma

    public bool stopWhenFalling;

    Vector3 offset;     //vector nv -> camera (const)

    float lowY;

    GameObject fox;


    // Use this for initialization
    void Start()
    {
        fox = GameObject.FindGameObjectWithTag(GameDefine.TAG_PLAYER);
        target = fox.transform;
        offset = transform.position - target.position;

        lowY = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target.position.x > posTranLeft.position.x && target.position.x < posTranRight.position.x)
        {
            Vector3 targetCamPos = target.position + offset; // vi tri camera moi

            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime); // muot ma

            if (stopWhenFalling)
                if (transform.position.y < lowY) transform.position = new Vector3(transform.position.x, lowY, transform.position.z); // dung camera khi roi
        }    
    }
}

