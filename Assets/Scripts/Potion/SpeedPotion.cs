using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpeedPotion : MonoBehaviour, IPotion
{

    public float increasesSpeed = 0.25f;
    public float useTime = 5;


    public void Use(Agent user)
    {
        float speedToAdd = Mathf.Round(user.speed * increasesSpeed);

        user.speed += speedToAdd;
        StartCoroutine(RemoveSpeedEffect(user, speedToAdd));
    }

    public IEnumerator RemoveSpeedEffect(Agent user, float speedToRemove) {
        
        yield return new WaitForSeconds(useTime);
        user.speed -= speedToRemove;
    }
}
