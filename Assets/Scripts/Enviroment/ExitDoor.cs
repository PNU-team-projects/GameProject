using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class ExitDoor : Door
{
    public float fadeSpeed = 2f;
    private Color initialColor;

    public SpriteRenderer spriteRender;
    public UnityEvent exitDoorEvent;

    private Collider2D collider;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        collider.enabled = false;
        initialColor = spriteRender.color;

    }



    void Update()
    {
        if (isOpen)
        {
            spriteRender.color = Color.Lerp(spriteRender.color, new Color(initialColor.r, initialColor.g, initialColor.b, 0f), fadeSpeed * Time.deltaTime);
        }
        else
        {
            spriteRender.color = Color.Lerp(spriteRender.color, initialColor, fadeSpeed * Time.deltaTime);
        }
    }

    public override void Open()
    {
        if (isOpen) return;
        base.Open();
        collider.enabled = true;
    }

    public override void Close()
    {
        return;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            exitDoorEvent?.Invoke();
        }
    }
}
