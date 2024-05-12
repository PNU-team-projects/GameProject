using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponized
{
    IWeapon activeWeapon { get; set; }
    int damageBonus { get; set; }

    void Attack();

}
