using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    float speed { get; set; }

    void Move(Vector2 movement);

}
