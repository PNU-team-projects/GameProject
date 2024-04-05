using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : IPotion
{

    private static HealingPotion _instance;
    public static HealingPotion Instance => _instance ??= new HealingPotion();

    public float recovers = 0.25f;


    public void Use(Agent user)
    {
        int hpToRecover = Mathf.RoundToInt(user.maxHP * recovers);

        user.AddHP(hpToRecover);
    }
}
