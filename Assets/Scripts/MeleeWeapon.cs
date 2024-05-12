using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon: MonoBehaviour, IWeapon
{

    [field: SerializeField] public float knockBackForce { get; set; }
    [field: SerializeField] public int damage { get; set; }
    [field: SerializeField] public float CD { get; set; }
    [SerializeField] public DamageSource hitCollider;


    private Animator animator;
    private int damageBonus = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        hitCollider.damageEvent += OnDamage;
    }


    public void Attack(int damageBonus) {
        this.damageBonus = damageBonus;
        animator.SetTrigger("Attack");
    }

    public void ActivateHitCollider()
    {
        hitCollider.gameObject.SetActive(true);
    }

    public void AttackDone()
    {
        hitCollider.gameObject.SetActive(false);
    }

    private void OnDamage(IDamageable damageable) {
        damageable.TakeDamage(this.damage + this.damageBonus);
        damageable.GetKnockBack(this.knockBackForce, transform);
    }
}
