using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entered the shop trigger");
            ShopManagerScript.instance.ToggleShop();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Exited the shop trigger");
            ShopManagerScript.instance.ToggleShop();
        }
    }
}
