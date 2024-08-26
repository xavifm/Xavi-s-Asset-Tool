using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Entity
{
    public override void InteractWith()
    {
        Debug.Log("BANG!");
    }
}
