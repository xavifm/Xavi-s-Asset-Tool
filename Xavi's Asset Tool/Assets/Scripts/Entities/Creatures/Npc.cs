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

    public override void LifeLogic()
    {
        base.LifeLogic();

        if (Life <= 0)
        {
            NpcMovementEntity movementNpcAux = (NpcMovementEntity) MovementLogic;
            movementNpcAux.SwitchState(NpcMovementEntity.AnimationStates.DEAD);
        }

    }
}

