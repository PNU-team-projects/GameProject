using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent
{

    private IPotion activePotion;
    public float potionCD = 0;


    private new void Awake()
    {
        base.Awake();
        
        // for test
        activePotion = gameObject.GetComponentInChildren<SpeedPotion>();
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
    }

 
    public new void DeathDone()
    {
        isDying = false;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(potionCD > 0)
        {
            potionCD -= Time.deltaTime;
        }
    }

    public void UsePotion()
    {
        if (potionCD > 0) return;

        activePotion.Use(this);
        potionCD = activePotion.CD;
    }
}
