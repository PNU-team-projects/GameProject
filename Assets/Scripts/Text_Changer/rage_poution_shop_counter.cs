using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class rage_poution_shop_counter : MonoBehaviour
{
    public ActiveInventory activeInventory;
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (activeInventory != null)
        {
            textMeshPro.text = activeInventory.rage_p.ToString();
        }
    }
}