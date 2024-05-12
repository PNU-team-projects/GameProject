using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : Door
{

    private Animator animator;
    private Collider2D collider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();

    }

    public override void Open()
    {
        if (isOpen) return;
        base.Open();

        animator.SetTrigger("Open");
    }

    public void OnOpenAnimationFinished()
    {
        collider.enabled = false;
    }


    public override void Close()
    {
        if (!isOpen) return;
        base.Close();

        collider.enabled = true;
        animator.SetTrigger("Closed");
    }

}
