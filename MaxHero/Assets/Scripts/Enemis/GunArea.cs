using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunArea : MonoBehaviour
{
    [SerializeField]
    GunStock gun;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gun.OnSpawn();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gun.OffSpawn();
        }
    }
}
