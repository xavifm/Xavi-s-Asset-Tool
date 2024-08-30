using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CreatureEntity
{
    [SerializeField] KeyCode ActionKey;
    [SerializeField] KeyCode PauseKey;
    [SerializeField] KeyCode InteractKey;
    [SerializeField] KeyCode InteractKeySecondary;
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
            Entity entityOnHand = HandItem.CurrentHandItem;

            WeaponsInteractionLogic(entityOnHand);
            GlobalInteractionLogic(entityOnHand);

            if (Input.GetKeyDown(ActionKey))
                PickupEntity();
        }
    }

    private void WeaponsInteractionLogic(Entity _handEntity)
    {
        if (_handEntity == null)
            return;

        if (_handEntity.TypeOfEntity.Equals(EntityType.WEAPON))
        {
            Weapon handWeapon = (Weapon) _handEntity;

            if (Input.GetKey(InteractKey))
                handWeapon.InteractWithWeapon(InteractKey);

            if (Input.GetKey(InteractKeySecondary))
                handWeapon.InteractWithWeapon(InteractKeySecondary);
        }
    }

    private void GlobalInteractionLogic(Entity _handEntity)
    {
        if (_handEntity == null)
            return;

        if (Input.GetKeyDown(InteractKey))
        {
            _handEntity.InteractWith();

            if (_handEntity.TypeOfEntity.Equals(EntityType.WORLD_PROP))
            {
                HandItem.ThrowHandItem(InventoryLogic);
                Entity nextEntity = InventoryLogic.GetItemByIdentity(_handEntity);
                HandItem.SetHandItem(nextEntity);
            }
        }
    }
}
