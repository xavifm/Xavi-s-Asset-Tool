using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ItemUI : MonoBehaviour
{
    public string Name;
    public Text Description;

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
        Name = _object.Name;
        Description = _object.Description;
    }
}
