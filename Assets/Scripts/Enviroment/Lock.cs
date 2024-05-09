using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public Door door;
    public KeyCode keyCode = KeyCode.E;
    private bool playerNearby = false;
    public Sprite openedSprite;

    private SpriteRenderer spriteRenderer;
    private Collider2D collider;
    private bool isOpen = false;
    private Player lastPlayer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {

        if (playerNearby && Input.GetKeyDown(keyCode))
        {

            if (!isOpen && lastPlayer.Keys > 0)
            {
                door.Open();
                isOpen = true;
                spriteRenderer.sprite = openedSprite;
                collider.enabled = false;
                lastPlayer.Keys--;

            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            lastPlayer = other.GetComponent<Player>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
