using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : MonoBehaviour, IDamageable, IMovable, IWeaponized
{
    [SerializeField] public int maxHP = 3;
    [SerializeField] private int currentHP;

    [field: SerializeField] public float speed { get; set; } = 2f;

    [field: SerializeField] public IWeapon activeWeapon { get; set; }
    [field: SerializeField] public int damageBonus { get; set; } = 0;
    [SerializeField] private GameObject weaponContainer;
    [SerializeField] protected float knockbackTime = 0.2f;
    


    protected bool isDying { get; set; }
    protected bool isKnockedBack { get; set; }
    protected Vector2 movement;
    [SerializeField] private float attackCD = 0;

    protected FlashEffect flashEffect;
    protected Rigidbody2D rb;
    protected Animator animator;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        flashEffect = GetComponent<FlashEffect>();

        currentHP = maxHP;
        // for test
        activeWeapon = weaponContainer.GetComponentInChildren<IWeapon>();
    }
    protected void Update()
    {
        if (attackCD > 0)
        {
            attackCD -= Time.deltaTime;
        }
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

    public int GetCurrentHP()
    {
        return currentHP;
    }

    public void AddHP(int hpToAdd)
    {
        int diff = maxHP - currentHP;
        if (diff >= hpToAdd)
        {
            currentHP += hpToAdd;
        } else
        {
            currentHP += diff;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDying) return;

        currentHP -= damage;
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
        if (currentHP <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        animator.SetTrigger("Death");
        isDying = true;
        weaponContainer.SetActive(false);
    }

    public void DeathDone()
    {
        isDying = false;
        Destroy(gameObject);
    }

    public void Attack()
    {
        if (attackCD > 0) return;

        activeWeapon.Attack(this.damageBonus);
        attackCD = 1;
    }
}
