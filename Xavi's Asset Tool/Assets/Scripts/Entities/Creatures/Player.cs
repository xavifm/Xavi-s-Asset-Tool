using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CreatureEntity
{
    [SerializeField] KeyCode ActionKey;
    [SerializeField] KeyCode PauseKey;
    [SerializeField] KeyCode InteractKey;
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

        if(!MenuSystem.HaveOpenMenu)
        {
            if (Input.GetKeyDown(InteractKey))
            {
                Entity entityOnHand = HandItem.CurrentHandItem;

                if(entityOnHand != null)
                    entityOnHand.InteractWith();

                if (entityOnHand.TypeOfEntity.Equals(EntityType.WORLD_PROP))
                {
                    HandItem.ThrowHandItem(InventoryLogic);
                    Entity nextEntity = InventoryLogic.GetItemByIdentity(entityOnHand);
                    HandItem.SetHandItem(nextEntity);
                }
            }

            if (Input.GetKeyDown(ActionKey))
                PickupEntity();
        }
    }
}
