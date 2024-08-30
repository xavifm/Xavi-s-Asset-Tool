using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Entity
{
    public Transform RayCastPoint;

    public override void VirtualUpdate()
    {
        base.VirtualUpdate();
    }

    public virtual void InteractWithWeapon(KeyCode _key)
    {

    }
}
