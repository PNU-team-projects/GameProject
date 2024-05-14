using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class ShopManagerScript : MonoBehaviour
{
    public TextMeshProUGUI CoinsTXT;
    public Player player;

    public int[,] shopItems = new int[6, 6];
    public ActiveInventory activeInventory; // Додайте посилання на ActiveInventory

    public int startingCoins = 100;
    public static ShopManagerScript instance;

    public GameObject ShopUI;

    void Start()
    {
        // Ініціалізуємо кількість монет гравця
        //player.Coins = startingCoins;

        // Ініціалізуйте ActiveInventoryInstance, якщо він ще не ініціалізований
        if (activeInventory == null)
        {
            activeInventory = Object.FindFirstObjectByType<ActiveInventory>();
        }

        //ID's

        shopItems[1, 1] = 1;

        shopItems[1, 2] = 2;

        shopItems[1, 3] = 3;

        shopItems[1, 4] = 4;

        shopItems[1, 5] = 5;


        //Price

        shopItems[2, 1] = 1;

        shopItems[2, 2] = 2;

        shopItems[2, 3] = 3;

        shopItems[2, 4] = 4;

        shopItems[2, 5] = 5;

        //Quantity

        shopItems[3, 1] = 0;

        shopItems[3, 2] = 0;

        shopItems[3, 3] = 0;

        shopItems[3, 4] = 0;

        shopItems[3, 5] = 0;


        // Оновлення тексту кількості монет
        UpdateCoinsText();

    }

    // Не використовується !!!
    void UpdateCoinsText()
    {
        CoinsTXT.text = "Coins: " + player.Coins.ToString();
    }

    //покупки предметів у магазині
    public void Buy()
    {
        GameObject ButtonRef = EventSystem.current.currentSelectedGameObject;

        if (player.Coins >= GetItemPrice(ButtonRef))
        {
            player.Coins -= GetItemPrice(ButtonRef);
            IncrementItemQuantity(ButtonRef);
            UpdateCoinsText();
            UpdateItemQuantityText(ButtonRef);

            // Оновлення лічильників зілля в інвентарі 
            UpdatePotionCount(ButtonRef);
        }
    }
    int GetItemPrice(GameObject button)
    {
        int itemID = button.GetComponent<ButtonInfo>().ItemID;
        return shopItems[2, itemID];
    }

    void IncrementItemQuantity(GameObject button)
    {
        int itemID = button.GetComponent<ButtonInfo>().ItemID;
        shopItems[3, itemID]++;
    }

    void UpdateItemQuantityText(GameObject button)
    {
        int itemID = button.GetComponent<ButtonInfo>().ItemID;
        button.GetComponent<ButtonInfo>().QuantityTxt.text = shopItems[3, itemID].ToString();
    }

    // Оновлення лічильників зілля в інвентарі при купівлі
    void UpdatePotionCount(GameObject button)
    {
        int itemID = button.GetComponent<ButtonInfo>().ItemID;
        switch (itemID)
        {
            case 1: // Якщо куплено предмет з індексом 1
                activeInventory.health_p++; // зілля здоров'я +1
                break;
            case 2: // Якщо куплено предмет з індексом 2
                activeInventory.rage_p++; // зілля сили +1
                break;
            case 3: // Якщо куплено предмет з індексом 3
                activeInventory.speed_p++; // зілля швидкості +1
                break;
            case 4: // Якщо куплено предмет з індексом 4
                    // Збільшення максимального здоров'я на 5
                player.maxHP += 5;
                // Встановлення поточного здоров'я рівним новому значенню максимального
                player.currentHP = player.maxHP;
                break;
            case 5: // Якщо куплено предмет з індексом 5
                player.damageBonus += 1; // збільшення урону персонажа на 1
                break;
            default:
                break;
        }
    }
    private void Awake()
    {

        if (instance == null)
        {

            instance = this;

        }
        else
        {

            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);
    }

    public void ToggleShop()
    {

        ShopUI.SetActive(!ShopUI.activeSelf);

    }

}