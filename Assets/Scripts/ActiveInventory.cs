using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private int activeSlotIndexNum = 0;
    private PlayerControls playerControls;
    public string activeTag; // Added variable to store the tag

    // Health, Rage, Speed potions counters
    public int health_p = 0;
    public int rage_p = 0;
    public int speed_p = 0;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        // Check the potion counters and set the corresponding child active or inactive
        this.transform.GetChild(0).gameObject.SetActive(health_p > 0);
        this.transform.GetChild(1).gameObject.SetActive(rage_p > 0);
        this.transform.GetChild(2).gameObject.SetActive(speed_p > 0);
    }

    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighLight(numValue - 1);
    }

    private void ToggleActiveHighLight(int indexNum)
    {
        activeSlotIndexNum = indexNum;

        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        var activeChild = this.transform.GetChild(indexNum).GetChild(0);
        activeChild.gameObject.SetActive(true);
        activeTag = activeChild.tag;
    }
}
