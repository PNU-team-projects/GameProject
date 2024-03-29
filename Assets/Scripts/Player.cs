using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent, IWeaponized
{
    [field: SerializeField] public IWeapon activeWeapon { get; set; }
    [SerializeField] private GameObject weaponContainer;


    private new void Awake()
    {
        base.Awake();
        activeWeapon = weaponContainer.GetComponentInChildren<IWeapon>();
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
        //base.Death();
        animator.SetTrigger("Death");
        gameObject.SetActive(false);
        weaponContainer.SetActive(false);
    }

    public void Attack()
    {
        activeWeapon.Attack();
    }
}
