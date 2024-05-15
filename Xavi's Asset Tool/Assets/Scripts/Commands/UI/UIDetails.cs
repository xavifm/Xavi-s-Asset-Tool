using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDetails : UICommand
{
    public LegendDetail Legend;

    public override void Execute()
    {
        Legend.SwitchEntity(GetComponent<ItemUI>().Entity);
    }
}
