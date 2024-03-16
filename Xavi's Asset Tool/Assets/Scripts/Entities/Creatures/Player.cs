using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CreatureEntity
{
    [SerializeField] private CreatureMovement MovementLogic;

    public override void VirtualUpdate()
    {
        base.VirtualUpdate();
        MovementLogic.MovementLogic();
        MovementLogic.RotationLogic();
        MovementLogic.JumpLogic();
    }
}
