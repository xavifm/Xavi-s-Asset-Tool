using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : CreatureEntity
{

    public override void VirtualUpdate()
    {
        base.VirtualUpdate();
        NpcMovementEntity movementCast = (NpcMovementEntity) MovementLogic;

        movementCast.MovementLogic();
        movementCast.RotationLogic();
        movementCast.JumpLogic();
        movementCast.AnimationLogic();
        movementCast.CollisionLogic();
    }
}

