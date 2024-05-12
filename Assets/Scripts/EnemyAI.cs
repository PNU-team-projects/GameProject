using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    public Transform[] spots;
    public float timeToWait = 3;

    public PlayerController player;
    public float chaseDistanceThreshold = 6f;
    public float attackDistanceThreshod = 2f;

    private float waitTime;
    private Vector2 randomSpot;

    private Enemy enemy;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void Start()
    {
        randomSpot = spots[Random.Range(0, spots.Length)].position;
        waitTime = timeToWait;
    }

    private void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (player.isActiveAndEnabled && distanceToPlayer < chaseDistanceThreshold)
        {
            Chase(distanceToPlayer);
        } else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        enemy.Unfocus();
        if (Vector2.Distance(transform.position, randomSpot) < 0.1)
        {
            enemy.Move(Vector2.zero);
            if (waitTime <= 0)
            {
                randomSpot = spots[Random.Range(0, spots.Length)].position;
                waitTime = timeToWait;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        } else
        {   
            enemy.Move((randomSpot - (Vector2)transform.position).normalized);
        }
    }

    private void Chase(float distanceToPlayer)
    {
        if (distanceToPlayer < attackDistanceThreshod)
        {
            if( enemy.activeWeapon is Bow)
            {
                enemy.Move(Vector2.zero);
                enemy.Target(player.transform.position);
            }

            enemy.Attack();
        }
        else
        {
            enemy.Move((player.transform.position - transform.position).normalized);
        }
    }

}
