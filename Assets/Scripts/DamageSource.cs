using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageSource : MonoBehaviour
{
    public event Action<IDamageable> damageEvent;

    private void OnTriggerEnter2D(Collider2D collider)
    {

        IDamageable damageable = collider.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageEvent?.Invoke(damageable);
        }
    }

}
