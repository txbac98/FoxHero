using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{

    public int HP;
    
    public GameObject bar;

    

    float ratio;

    private void Start()
    {
        if (bar != null)
        {
            ratio = bar.transform.localScale.x / HP;
        }
        
    }

    public void AddDame(int dama)
    {
        HP -= dama;
        if (bar!=null)
            bar.transform.localScale -= new Vector3(dama * ratio, 0);
    }
    
}
