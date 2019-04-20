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

    public static FoxHp Instance;

    public void Awake()
    {
        Instance = this;
    }
    public void OnHit(int dama)
    {
        bar.fillAmount -= (float)dama / HP;
        if (bar.fillAmount == 0)
        {
            FoxController.Instance.Death();
        }
    }
    public void AddHp(int hp)
    {
        bar.fillAmount += (float)hp / HP;
    }
    public void OnMaxHit()
    {
        bar.fillAmount = 0;
        FoxController.Instance.Death();
    }

}
