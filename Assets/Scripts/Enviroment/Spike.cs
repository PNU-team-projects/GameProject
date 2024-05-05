using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour, IWeapon
{

    [field: SerializeField] public float knockBackForce { get; set; }
    [field: SerializeField] public int damage { get; set; }
    public float CD { get; set; }
    public float delay = 0;

    private Animator animator;
    private Collider2D collider2D;
    private bool isActive = false;

    public void Attack(int damageBonus)
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();

        animator.enabled = false;
        StartCoroutine(Delay());
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        IDamageable damageable = collider.gameObject.GetComponent<IDamageable>();

        if (damageable != null && collider is BoxCollider2D)
        {
            damageable.TakeDamage(this.damage);
            damageable.GetKnockBack(this.knockBackForce, transform);
        }
    }
    

    public void ToggleSpike()
    {
        if (isActive)
        {
            this.collider2D.enabled = false;
        } else
        {
            this.collider2D.enabled = true;
        }
        this.isActive = !this.isActive;
    }


    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(this.delay);

        animator.enabled = true;
    }

}
