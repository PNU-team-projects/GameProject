using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpeedPotion : MonoBehaviour, IPotion
{
    [field: SerializeField] public float CD { get; set; } = 2;
    [SerializeField] private float increasesSpeed = 0.25f;
    [SerializeField] private float duration = 5;

    public void Use(Agent user)
    {
        float speedToAdd = Mathf.Round(user.speed * increasesSpeed);

        user.speed += speedToAdd;
        StartCoroutine(RemoveEffect(user, speedToAdd));
    }

    public IEnumerator RemoveEffect(Agent user, float speedToRemove) {
        
        yield return new WaitForSeconds(duration);
        user.speed -= speedToRemove;
    }
}
