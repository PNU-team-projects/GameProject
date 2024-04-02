using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;  // Reference to SpriteRenderer component
    [SerializeField] private GameObject destroyVFX;        // Reference to destroy VFX effect
    [SerializeField] private Sprite damagedSprite;         // Reference to damaged sprite
    [SerializeField] private Collider2D moveCollider; // Reference to Collider2D component (optional)
    [SerializeField] private Collider2D hitCollider; // Reference to Collider2D component (optional)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<DamageSource>())
        {
            GetComponent<PickUpSpawner>().DropItems();

            // Instantiate VFX effect
            Instantiate(destroyVFX, transform.position, Quaternion.identity);

            // Disable both colliders
            moveCollider.enabled = false;
            hitCollider.enabled = false;

            // Change sprite to damagedSprite
            spriteRenderer.sprite = damagedSprite;
        }
    }
}
