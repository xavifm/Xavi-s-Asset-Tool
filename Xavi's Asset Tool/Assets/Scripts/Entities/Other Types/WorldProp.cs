using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldProp : Entity
{
    public bool Explode;
    public bool Destroy;

    public override void VirtualUpdate()
    {
        base.VirtualUpdate();

        if (Explode)
            MovementLogic.Explode(Vector3.up, 10, 20);
        if (Destroy)
            MovementLogic.Destroy(0, true);

        Explode = false;
        Destroy = false;
    }
}
