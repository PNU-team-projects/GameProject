using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [field: SerializeField] public float knockBackForce { get; set; }
    [field: SerializeField] public int damage { get; set; }
    [field: SerializeField] public float CD { get; set; }
    [SerializeField] public float range = 20;

    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;
    

    public void Attack(int damageBonus)
    {
        Projectile projectile = Instantiate(arrowPrefab, arrowSpawnPoint.position, transform.rotation).GetComponent<Projectile>();
        projectile.setStats(damage+damageBonus, knockBackForce, range);
    }

    public void Target(Vector3 target)
    {
        Vector2 direction = transform.position - target;
        transform.right = -direction;
    }

    public void Unfocus(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
}
