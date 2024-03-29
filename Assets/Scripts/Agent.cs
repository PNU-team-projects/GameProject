using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : MonoBehaviour, IMovable, IDamageable
{
    [field: SerializeField] public float speed { get; set; } = 2f;
    [field: SerializeField] public int hp { get; set; } = 3;
    [SerializeField] protected float knockbackTime = 0.2f;

    protected bool isDying { get; set; }
    protected bool isKnockedBack { get; set; }
    protected Vector2 movement;

    protected FlashEffect flashEffect;
    protected Rigidbody2D rb;
    protected Animator animator;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        flashEffect = GetComponent<FlashEffect>();
    }

    public void Move(Vector2 movement)
    {
        if (isKnockedBack || isDying) return;
        this.movement = movement;
        AdjustPlayerFacingDirection();
        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);

        rb.MovePosition((rb.position + movement * (speed * Time.fixedDeltaTime)));
    }

    protected virtual void AdjustPlayerFacingDirection()
    {
        if (movement.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
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
            Death();
        }
    }

    public virtual void Death()
    {
        animator.SetTrigger("Death");
        isDying = true;
    }

    public void DeathDone()
    {
        isDying = false;
        Destroy(gameObject);
    }
}
