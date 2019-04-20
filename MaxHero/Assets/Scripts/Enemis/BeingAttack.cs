using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingAttack : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer sprite;

    [SerializeField]
    float timeOff;


    float time;
    public bool beingAttack;

    // Start is called before the first frame update
    void Start()
    {
        beingAttack = false;
        time = 0;    
    }

    // Update is called once per frame
    void Update()
    {
        if (beingAttack)
        {
            time += Time.deltaTime;
            if (time > timeOff)
            {
                sprite.color = Color.white;
                time = 0;
            }
        }      
    }
    
    public void SetRedColor()
    {
        sprite.color = Color.red;
        beingAttack = true;
    }
}
