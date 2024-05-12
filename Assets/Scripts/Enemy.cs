using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Agent
{

    public void Target(Vector3 target)
    {

        if (activeWeapon is Bow)
        {

            if (target.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, -180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            ((Bow)activeWeapon).Target(target);

        }
    }

    public void Unfocus()
    {
        if (activeWeapon is Bow)
        {
            ((Bow)activeWeapon).Unfocus(transform.rotation);
        }
    }
}
