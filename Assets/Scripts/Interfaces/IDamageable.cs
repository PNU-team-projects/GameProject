using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{

    int hp { get; set; }

    void TakeDamage(int damage);
    void GetKnockBack(float force, Transform source);

    void Death();

}
