using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target; // nv theo doi
    public float smoothing; //muot ma

    Vector3 offset;     //vector nv -> camera (const)

    float lowY;

    // Use this for initialization
    void Start()
    {

        offset = transform.position - target.position;

        lowY = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 targetCamPos = target.position + offset; // vi tri camera moi

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime); // muot ma

        if (transform.position.y < lowY) transform.position = new Vector3(transform.position.x, lowY, transform.position.z); // dung camera khi roi
    }
}

