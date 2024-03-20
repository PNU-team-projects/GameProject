using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;

    private Vector2 pointToMove;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    public void MoveTo(Vector2 targetPosition)
    {
        pointToMove = targetPosition;
    }

    private void AdjustPlayerFacingDirection()
    {

        if (transform.position.x < pointToMove.x) {
            spriteRenderer.flipX = false;

        } else if (transform.position.x > pointToMove.x)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void Move()
    {
        animator.SetFloat("moveX", transform.position.x - pointToMove.x);
        animator.SetFloat("moveY", transform.position.y - pointToMove.y);

        Vector2 newPosition = Vector2.MoveTowards(transform.position, pointToMove, Time.deltaTime * moveSpeed);
        rb.MovePosition(newPosition);
    }
}
