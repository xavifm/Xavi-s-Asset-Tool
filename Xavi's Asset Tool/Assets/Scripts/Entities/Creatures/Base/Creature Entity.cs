using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureEntity : Entity
{
    [SerializeField] protected Inventory InventoryLogic;
    [SerializeField] protected Hand HandItem;


    public virtual void PickupEntity()
    {
        CreatureMovement movement = (CreatureMovement) MovementLogic;
        Entity forwardEntity = movement.CheckPointingEntity();

        if (forwardEntity != null)
            InventoryLogic.StoreItem(forwardEntity, this);
    }

    public virtual void DropEntity()
    {

    }
}
