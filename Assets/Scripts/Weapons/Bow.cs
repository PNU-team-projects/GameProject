using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    public float knockBackForce { get; set; }
    public int damage { get; set; }


    void Start()
    {
        
    }


    void Update()
    {
        MouseFollowingWithOffset();
    }

    private void MouseFollowingWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector2 direction = transform.position  - Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.right = -direction;

        //float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, angle);

    }

    public void Attack(int damageBonus)
    {

        //Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = rotation;
        return;
    }
}
