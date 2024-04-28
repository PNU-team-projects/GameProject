using UnityEngine;
using TMPro;

public class Key_change_amount : MonoBehaviour
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
            textMeshPro.text = playerScript.Keys.ToString();
        }
    }
}
