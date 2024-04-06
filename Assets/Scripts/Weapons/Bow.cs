using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [field: SerializeField] public float knockBackForce { get; set; }
    [field: SerializeField] public int damage { get; set; }
    [SerializeField] public float range = 20;

    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;


    void Update()
    {
        MouseFollowingWithOffset();
    }

    private void MouseFollowingWithOffset()
    {
        Vector2 direction = transform.position  - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.right = -direction;
    }

    public void Attack(int damageBonus)
    {
        Projectile projectile = Instantiate(arrowPrefab, arrowSpawnPoint.position, transform.rotation).GetComponent<Projectile>();
        projectile.setStats(damage+damageBonus, knockBackForce, range);
    }
}
