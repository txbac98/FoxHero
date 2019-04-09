using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GunIcon
{
    public int id;
    public float timeDelay;
    public float angleLimit;
    public Sprite spr;
}

public class GunManager : MonoBehaviour
{
    public static GunManager Instance;
    [SerializeField]
    Gun gun;

    public SpriteRenderer sprGun;
    public GameObject panelGun;
    public List<GunIcon> gunIcons;

    public float time;
    public float angleLimit;
    public float timeDelay;
    public bool canShoot;

    private void Awake()
    {
        GunManager.Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        canShoot = true;
        //this.ChangeGun(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ChangeGun(0);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                ChangeGun(1);
            }
        }
    }
    public void RotationGun(float angle)
    {
        if(this.gun != null)
        {
            this.gun.RotationGun(angle);
            this.panelGun.transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
        
    }
    public void Fire()
    {
        if (this.gun != null)
        {            
            if (canShoot)
            {
                time += Time.deltaTime;
                if (time > timeDelay)
                {
                    this.gun.Fire(this.transform.position);
                    time = 0;
                }
            }                  
        }
    }
    public void ChangeGun(int ID)
    {
        this.gun = new Gun(gunIcons[ID].id, gunIcons[ID].timeDelay);
        //this.gun.RotationGun();
        this.sprGun.sprite = gunIcons[ID].spr;
        timeDelay = gunIcons[ID].timeDelay;
        angleLimit = gunIcons[ID].angleLimit;
        RotationGun(0);

    }
    public Sprite GetSpriteGunById()
    {
        if(this.gun != null)
        {
            foreach(GunIcon gun in this.gunIcons)
            {
                if(gun.id == this.gun.id)
                {
                    return gun.spr;
                }
            }
        }
        return null;
    }

}
