using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPotion
{
    float CD { get; set; }

    void Use(Agent user);
}
