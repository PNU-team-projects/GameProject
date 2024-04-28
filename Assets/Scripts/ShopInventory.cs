using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShopInventory : MonoBehaviour
{
    private PlayerControls playerControls;
    public GameObject shopInventory;
    public static bool shopIsInactive = true;
    private int currentIndex = 0;

    private void Awake()
    {
        playerControls = new PlayerControls();
        shopInventory.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (shopIsInactive)
            {
                OpenShop();
            }
            else
            {
                CloseShop();
            }
        }
    }

    private void Start()
    {
        playerControls.ShopInventory.NavigateLeft.performed += ctx => Navigate(-1);
        playerControls.ShopInventory.NavigateRight.performed += ctx => Navigate(1);
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    public void OpenShop()
    {
        shopIsInactive = false;
        shopInventory.SetActive(true);
    }

    public void CloseShop()
    {
        shopIsInactive = true;
        shopInventory.SetActive(false);
    }

    private void Navigate(int indexNum)
    {
        // Get all the ShopInventoryItemBox objects
        GameObject[] shopInventoryItemBoxes = GameObject.FindGameObjectsWithTag("ItemBox");

        // Deactivate the current active item
        shopInventoryItemBoxes[currentIndex].transform.Find("Active").gameObject.SetActive(false);

        // Update the currentIndex
        currentIndex += indexNum;
        if (currentIndex < 0)
        {
            currentIndex = shopInventoryItemBoxes.Length - 1; // Wrap around to the last item
        }
        else if (currentIndex >= shopInventoryItemBoxes.Length)
        {
            currentIndex = 0; // Wrap around to the first item
        }

        // Activate the new current item
        shopInventoryItemBoxes[currentIndex].transform.Find("Active").gameObject.SetActive(true);
    }

}
