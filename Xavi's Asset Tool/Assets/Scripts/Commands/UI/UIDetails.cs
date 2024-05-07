using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDetails : UICommand
{
    public LegendDetail Legend;
    public Entity UIEntity;

    public override void Execute()
    {
        Legend.SwitchEntity(UIEntity);
    }
}
