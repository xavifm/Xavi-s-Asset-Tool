using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CreatureEntity
{
    [SerializeField] KeyCode ActionKey;
    [SerializeField] KeyCode PauseKey;
    [SerializeField] KeyCode ThrowKey;
    [SerializeField] MenuManager MenuSystem;

    public override void VirtualUpdate()
    {
        base.VirtualUpdate();
        PlayerMovementEntity movementCast = (PlayerMovementEntity) MovementLogic;

        movementCast.MovementLogic();
        movementCast.RotationLogic();
        movementCast.JumpLogic();

        if (Input.GetKeyDown(PauseKey))
            MenuSystem.OpenMenu("PAUSE");

        if (Input.GetKeyDown(ThrowKey))
            HandItem.ThrowHandItem(InventoryLogic);

        if (Input.GetKeyDown(ActionKey))
            PickupEntity();
    }
}
