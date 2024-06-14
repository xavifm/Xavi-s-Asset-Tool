using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldProp : Entity
{
    public bool Enabled = false;
    public bool Explode;
    public bool Destroy;

    public override void VirtualUpdate()
    {
        if (!Enabled)
            return;

        base.VirtualUpdate();

        PropMovement propMovement = (PropMovement) MovementLogic;

        propMovement.MovementLogic();

        if (Explode)
            propMovement.Explode(Vector3.up, 10, 20);
        if (Destroy)
            propMovement.Destroy(0, true);

        Explode = false;
        Destroy = false;
    }
}
