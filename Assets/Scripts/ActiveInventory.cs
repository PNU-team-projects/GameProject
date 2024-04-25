using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private int activeSlotIndexNum = 0;

    private PlayerControls playerControls;
    public static ActiveInventory ActiveInventoryInstance;

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

    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighLight(numValue - 1);
    }

    private void ToggleActiveHighLight(int indexNum)
    {
        if (this == null)
        {
            // Create a new GameObject and add the ActiveInventory script to it
            GameObject inventoryObject = new GameObject("ActiveInventory");
            ActiveInventoryInstance = inventoryObject.AddComponent<ActiveInventory>();
            return;
        }

        activeSlotIndexNum = indexNum;

        // Check if the indexNum is within the range of children count
        if (this.transform.childCount > indexNum)
        {
            // Deactivate all inventory slots
            foreach (Transform inventorySlot in this.transform)
            {
                if (inventorySlot != null && inventorySlot.childCount > 0)
                {
                    GameObject childGameObject = inventorySlot.GetChild(0).gameObject;
                    if (childGameObject != null)
                    {
                        childGameObject.SetActive(false);
                    }
                }
            }

            // Activate the selected inventory slot
            Transform child = this.transform.GetChild(indexNum);
            if (child != null && child.childCount > 0)
            {
                GameObject childGameObject = child.GetChild(0).gameObject;
                if (childGameObject != null)
                {
                    childGameObject.SetActive(true);
                }
            }
        }
    }

}