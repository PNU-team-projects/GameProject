using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float fadeSpeed = 2f; // Швидкість зникнення/з'явлення дверей
    public KeyCode openKey = KeyCode.E; // Клавіша для відкриття дверей
    public SpriteRenderer doorRenderer; // Спрайт дверей

    private bool isOpen = false; // Прапорець, що вказує на те, чи відкриті двері
    private bool playerNearby = false; // Прапорець, що вказує на те, чи знаходиться гравець біля дверей
    private Color initialColor; // Початковий колір спрайту дверей

    void Start()
    {
        initialColor = doorRenderer.color;
    }

    void Update()
    {
        // Логіка відкриття/закриття дверей
        if (Input.GetKeyDown(openKey) && playerNearby)
        {
            isOpen = !isOpen;
        }

        // Зміна прозорості спрайту в залежності від стану дверей
        if (isOpen)
        {
            doorRenderer.color = Color.Lerp(doorRenderer.color, new Color(initialColor.r, initialColor.g, initialColor.b, 0f), fadeSpeed * Time.deltaTime);
        }
        else
        {
            doorRenderer.color = Color.Lerp(doorRenderer.color, initialColor, fadeSpeed * Time.deltaTime);
        }
    }

    // Обробник зіткнення з гравцем
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    // Обробник виходу гравця з зони дії
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
