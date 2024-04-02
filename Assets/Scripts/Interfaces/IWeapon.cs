using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon 
{
    float knockBackForce { get; set; }
    int damage { get; set; }

    void Attack();
}
