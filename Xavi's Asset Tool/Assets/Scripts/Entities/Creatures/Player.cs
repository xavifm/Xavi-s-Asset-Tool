using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CreatureEntity
{
    [SerializeField] KeyCode ActionKey;
    [SerializeField] KeyCode PauseKey;
    [SerializeField] KeyCode ThrowKey;
    [SerializeField] MenuManager MenuSystem;

    const float SCROLL_MULTIPLIER = 10;

    public override void VirtualUpdate()
    {
        base.VirtualUpdate();
        PlayerMovementEntity movementCast = (PlayerMovementEntity) MovementLogic;

        movementCast.MovementLogic();
        movementCast.RotationLogic();
        movementCast.JumpLogic();

        float mouseScrollAxis = Mathf.Clamp(Input.GetAxis("Mouse ScrollWheel") * SCROLL_MULTIPLIER, -1, 1);

        if (mouseScrollAxis != 0)
        {
            Entity entityQuery = InventoryLogic.GetNextWeapon(HandItem.CurrentHandItem, HandItem.FastEquipableItems, mouseScrollAxis);
            HandItem.SetHandItem(entityQuery);
        }

        if (Input.GetKeyDown(PauseKey))
            MenuSystem.OpenMenu("PAUSE");

        if (Input.GetKeyDown(ThrowKey))
            HandItem.ThrowHandItem(InventoryLogic);

        if (Input.GetKeyDown(ActionKey))
            PickupEntity();
    }
}
