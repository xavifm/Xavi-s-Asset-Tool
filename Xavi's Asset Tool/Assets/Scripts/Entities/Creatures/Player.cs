using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CreatureEntity
{
    [SerializeField] private CreatureMovement MovementLogic;
    [SerializeField] private Inventory InventoryLogic;

    public override void VirtualUpdate()
    {
        base.VirtualUpdate();
        MovementLogic.MovementLogic();
        MovementLogic.RotationLogic();
        MovementLogic.JumpLogic();
    }
}
