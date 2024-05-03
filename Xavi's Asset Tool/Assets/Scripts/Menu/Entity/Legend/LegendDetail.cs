using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LegendDetail : MonoBehaviour
{
    public Entity CurrentEntity;

    [SerializeField] UIDropItem DropCommand;
    [SerializeField] UIEquip EquipCommand;

    [SerializeField] TextMeshProUGUI NameText;
    [SerializeField] TextMeshProUGUI LegendText;

    public void SwitchEntity(Entity _entity)
    {
        CurrentEntity = _entity;

        NameText.text = CurrentEntity.Name;
        LegendText.text = CurrentEntity.Description;
    }
}
