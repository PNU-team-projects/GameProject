using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    public Transform[] spots;
    public float timeToWait = 3;

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
        enemy.MoveTo(randomSpot);

        if (Vector2.Distance(transform.position, randomSpot) < 0.1)
        {
            if (waitTime <= 0)
            {
                randomSpot = spots[Random.Range(0, spots.Length)].position;
                waitTime = timeToWait;
            } else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

}
