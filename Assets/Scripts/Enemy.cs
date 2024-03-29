using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IMovable, IDamageable, IWeaponized
{
    [field: SerializeField] public float speed { get; set; } = 2f;

    [field: SerializeField] public int hp { get; set; } = 3;
    [SerializeField] private float knockbackTime = 0.2f;
    [field: SerializeField] public IWeapon activeWeapon { get; set; }
    [SerializeField] private GameObject weaponContainer;


    private bool isDying { get;  set; }
    private bool isKnockedBack { get; set; }

    private FlashEffect flashEffect;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        flashEffect = GetComponent<FlashEffect>();
        activeWeapon = weaponContainer.GetComponentInChildren<IWeapon>();
    }


    public void Move(Vector2 movement)
    {
        if (isKnockedBack || isDying) return;

        AdjustPlayerFacingDirection(movement);
        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);

        rb.MovePosition((rb.position + movement * (speed * Time.fixedDeltaTime)));
    }

    private void AdjustPlayerFacingDirection(Vector2 movement)
    {
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
            weaponContainer.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
            weaponContainer.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDying) return;

        hp -= damage;
        StartCoroutine(flashEffect.Perform(DetectDeath));
    }

    public void GetKnockBack(float force, Transform damageSource)
    {
        if (isDying) return;

        isKnockedBack = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * force * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockBackCoroutine());
    }

    private IEnumerator KnockBackCoroutine()
    {
        yield return new WaitForSeconds(knockbackTime);
        rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }


    private void DetectDeath()
    {
        if (hp <= 0)
        {
            animator.SetTrigger("Death");
            isDying = true;
            weaponContainer.SetActive(false);
        }
    }

    public void Death()
    {
        isDying = false;
        Destroy(gameObject);
    }

    public void Attack()
    {
        this.activeWeapon.Attack();
    }
}
