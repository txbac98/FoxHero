using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoxHp : MonoBehaviour
{
    [SerializeField]
    int HP;
    [SerializeField]
    Image bar;


    public void AddDame(int dama)
    {
        bar.fillAmount -= (float)dama / HP;
    }
}
