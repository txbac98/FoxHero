using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{
    [SerializeField]
    int HP;
    [SerializeField]
    GameObject bar;

    float ratio;

    private void Start()
    {
        ratio = bar.transform.localScale.x/HP;
    }

    public virtual void AddDame(int dama)
    {
        HP -= dama;
        bar.transform.localScale -= new Vector3(dama * ratio, 0);
    }

}
