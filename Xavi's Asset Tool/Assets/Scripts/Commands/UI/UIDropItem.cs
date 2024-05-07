using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDropItem : UICommand
{
    [SerializeField] LegendDetail Legend;
    [SerializeField] Inventory InventoryLogic;

    public override void Execute()
    {
        if(Legend.CurrentEntity != null)
            InventoryLogic.RetrieveItem(Legend.CurrentEntity);
    }
}
