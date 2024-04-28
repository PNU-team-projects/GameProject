using UnityEngine;
using TMPro;

public class Key_change_amount1 : MonoBehaviour
{
    public Player playerScript;
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (playerScript != null)
        {
            textMeshPro.text = $"{playerScript.currentHP}/{playerScript.maxHP}";
        }
    }
}
