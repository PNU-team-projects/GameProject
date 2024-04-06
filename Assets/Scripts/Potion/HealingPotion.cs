using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : MonoBehaviour, IPotion
{

    public float healsHealth = 0.25f;


    public void Use(Agent user)
    {
        int hpToHeal = Mathf.RoundToInt(user.maxHP * healsHealth);
        user.AddHP(hpToHeal);
    }
}
