using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{

    [field: SerializeField] public float knockBackForce { get; set; }
    [field: SerializeField] public int damage { get; set; }
    [SerializeField] public GameObject hitCollider;


    private Animator animator;
    private DamageSource damageSource;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        damageSource = hitCollider.GetComponent<DamageSource>();

        damageSource.damageEvent += OnDamage;
    }


    public void Attack() {
        animator.SetTrigger("Attack");
    }

    public void ActivateHitCollider()
    {
        hitCollider.SetActive(true);
    }

    public void AttackDone()
    {
        hitCollider.SetActive(false);
    }

    private void OnDamage(IDamageable damageable) {
        damageable.TakeDamage(this.damage);
        damageable.GetKnockBack(this.knockBackForce, transform);
    }
}
