using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IWeaponized, IDamageable
{
    [field: SerializeField] public IWeapon activeWeapon { get; set; }
    [SerializeField] private GameObject weaponContainer;
    [SerializeField] private float moveSpeed = 1f;

    [field: SerializeField] public int hp { get; set; } = 3;
    [SerializeField] private float knockbackTime = 0.2f;

    private Vector2 movement;


    private bool isDying { get; set; }
    private bool isKnockedBack { get; set; }

    private FlashEffect flashEffect;
    private PlayerControls playerControls;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
        flashEffect = GetComponent<FlashEffect>();
        activeWeapon = weaponContainer.GetComponentInChildren<IWeapon>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput() {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);

    }

    private void Move()
    {
        if (isKnockedBack || isDying) return;
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }


    private void AdjustPlayerFacingDirection() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x) {
            mySpriteRender.flipX = true;
            weaponContainer.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            mySpriteRender.flipX = false;
            weaponContainer.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void Attack()
    {
        this.activeWeapon.Attack();
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
            myAnimator.SetTrigger("Death");
            isDying = true;
            weaponContainer.SetActive(false);
        }
    }

    public void Death()
    {
        isDying = false;
        gameObject.SetActive(false);
    }
}
