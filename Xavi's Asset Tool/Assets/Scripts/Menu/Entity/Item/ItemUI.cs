using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemUI : MonoBehaviour
{
    public string Name;
    public string Description;

    [SerializeField] TextMeshProUGUI NameText;

    public void OnEnable()
    {
        InitializeItem();
    }

    public void InitializeItem()
    {
        NameText.text = Name;
    }

    public void UpdateItem(Entity _object)
    {

    }
}
