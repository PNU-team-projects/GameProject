using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent, IWeaponized
{
    [field: SerializeField] public IWeapon activeWeapon { get; set; }

    private IPotion activePotion;

    [SerializeField] private GameObject weaponContainer;


    private new void Awake()
    {
        base.Awake();
        
        // for test
        activeWeapon = weaponContainer.GetComponentInChildren<IWeapon>();
        activePotion = gameObject.GetComponentInChildren<SpeedPotion>();
    }

     protected override void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public override void Death()
    {
        base.Death();
        weaponContainer.SetActive(false);
    }
    public new void DeathDone()
    {
        isDying = false;
        gameObject.SetActive(false);
    }

    public void Attack()
    {
        activeWeapon.Attack();
    }

    public void UsePotion()
    {
        activePotion.Use(this);
    }
}
