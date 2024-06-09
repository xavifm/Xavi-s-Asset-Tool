using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDropItem : UICommand
{
    [SerializeField] LegendDetail Legend;
    [SerializeField] Inventory InventoryLogic;
    [SerializeField] Hand PlayerHand;

    public override void Execute()
    {
        if(Legend.CurrentEntity != null)
        {
            PlayerHand.DropHandItem(Legend.CurrentEntity);
            InventoryLogic.RetrieveItem(Legend.CurrentEntity);

            Entity entityQuery = InventoryLogic.GetItemByIdentity(Legend.CurrentEntity);

            if(entityQuery != null)
                Legend.SwitchEntity(InventoryLogic.GetItemByIdentity(entityQuery));
        }
    }
}
