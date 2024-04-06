using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20;

    private int damage;
    private float knockBackForce;
    private float range;
    private Vector3 startingPosition;

    public void setStats(int damage, float knockBackForce, float range)
    {
        this.damage = damage;
        this.knockBackForce = knockBackForce;
        this.range = range;
    }

    private void Awake()
    {
        this.startingPosition = transform.position;
    }

    void Update()
    {
        MoveProjectile();
        DetectOutOfRange();
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
    private void DetectOutOfRange()
    {
        if (Vector3.Distance(startingPosition, transform.position) > range)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(this.damage);
            damageable.GetKnockBack(this.knockBackForce, transform);
            
        }
        Destroy(gameObject);
    }


}
