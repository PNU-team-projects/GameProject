using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Agent, IWeaponized
{
    [field: SerializeField] public IWeapon activeWeapon { get; set; }
    [SerializeField] private GameObject weaponContainer;


    private new void Awake()
    {
        base.Awake();
        activeWeapon = weaponContainer.GetComponentInChildren<IWeapon>();
    }

    public override void Death()
    {
        base.Death();
        weaponContainer.SetActive(false);
    }

    public void Attack()
    {
        activeWeapon.Attack();
    }
}
