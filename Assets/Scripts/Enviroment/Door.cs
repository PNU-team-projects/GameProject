using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public bool isOpen = false;


    public virtual void Open()
    {
        isOpen = true;
    }

    public virtual void Close()
    {
        isOpen = false;

    }

}
