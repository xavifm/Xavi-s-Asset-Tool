using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureEntity : Entity
{
    public int life;
    [SerializeField] protected Inventory InventoryLogic;

    public virtual void PickupEntity()
    {
        CreatureMovement movement = (CreatureMovement) MovementLogic;
        Entity forwardEntity = movement.CheckPointingEntity();

        if (forwardEntity != null)
            InventoryLogic.StoreItem(forwardEntity);
    }

    public virtual void DropEntity()
    {

    }
}
