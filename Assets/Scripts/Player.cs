using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent
{
    private IPotion activePotion;
    public float potionCD = 0;

    // Add Coins and Keys variables
    public int Coins = 0;
    public int Keys = 0;

    // Reference to the GameObject with the ActiveInventory script
    public GameObject activeInventoryObject;

    private new void Awake()
    {
        base.Awake();

        // potion
        //activePotion = gameObject.GetComponentInChildren<SpeedPotion>();
    }

    private new void Update()
    {
        base.Update();

        if (potionCD > 0)
        {
            potionCD -= Time.deltaTime;
        }
        // Check the tag and assign the corresponding potion
        var activeInventory = activeInventoryObject.GetComponent<ActiveInventory>();
        if (activeInventory != null)
        {
            string tag = activeInventory.activeTag;
            switch (tag)
            {
                case "H_potion":
                    activePotion = gameObject.GetComponentInChildren<HealingPotion>();
                    break;
                case "R_potion":
                    activePotion = gameObject.GetComponentInChildren<DamagePotion>();
                    break;
                case "S_potion":
                    activePotion = gameObject.GetComponentInChildren<SpeedPotion>();
                    break;
            }
        }
    }

    protected override void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (activeWeapon is Bow)
        {
            ((Bow)activeWeapon).Target(Camera.main.ScreenToWorldPoint(mousePos));
        }
    }

    public new void DeathDone()
    {
        isDying = false;
        gameObject.SetActive(false);
    }

    public void UsePotion()
    {
        if (potionCD > 0 || activePotion == null) return;

        activePotion.Use(this);
        potionCD = activePotion.CD;

        // Find the GameObject with the ActiveInventory script using its tag
        GameObject activeInventoryObject = GameObject.FindWithTag("ActiveInventoryTag");
        ActiveInventory activeInventory = null;
        if (activeInventoryObject != null)
        {
            activeInventory = activeInventoryObject.GetComponent<ActiveInventory>();
        }

        if (activeInventory != null) // Check if activeInventory is not null
        {
            if (activePotion is HealingPotion)
            {
                activeInventory.health_p--;
            }
            else if (activePotion is DamagePotion)
            {
                activeInventory.rage_p--;
            }
            else if (activePotion is SpeedPotion)
            {
                activeInventory.speed_p--;
            }
        }
    }
}
