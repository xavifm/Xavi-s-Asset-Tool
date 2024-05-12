using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEquip : UICommand
{
    [SerializeField] LegendDetail Legend;
    [SerializeField] Hand PlayerHand;

    public override void Execute()
    {
        if (Legend.CurrentEntity != null)
        {
            PlayerHand.SetHandItem(Legend.CurrentEntity);
        }
    }
}
