using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePotion : MonoBehaviour, IPotion
{
    [field: SerializeField] public float CD { get; set; } = 2;
    [SerializeField] private float increasesDamage = 0.25f;
    [SerializeField] private float duration = 5;


    public void Use(Agent user)
    {
        int damageToAdd = Mathf.RoundToInt((user.activeWeapon.damage+ user.damageBonus) * increasesDamage);

        user.damageBonus += damageToAdd;
        StartCoroutine(RemoveEffect(user, damageToAdd));
    }

    public IEnumerator RemoveEffect(Agent user, int damageToRemove)
    {
        yield return new WaitForSeconds(duration);
        user.damageBonus -= damageToRemove;
    }
}
