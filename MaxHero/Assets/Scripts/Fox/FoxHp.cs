using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoxHp : MonoBehaviour
{

    public int HP;
    [SerializeField]
    Image bar;



    private void Start()
    {
        HP = GameManager.instance.foxHp;
        if (HP == 0) HP = 100;
        bar.fillAmount = (float)HP / 100;
    }
    public void OnHit(int dama)
    {
        HP -= dama;
        bar.fillAmount -= (float)dama / (float)HP;
        if (bar.fillAmount <= 0 || HP<=0)
        {
            FoxController.Instance.Death();
        }
    }
    public void AddHp(int hp)
    {
        HP += hp;
        if (HP > 100)  HP=100;
        bar.fillAmount += (float)hp / HP;
    }
    public void OnMaxHit()
    {
        HP = 0;
        bar.fillAmount = 0;
        FoxController.Instance.Death();
    }

}
