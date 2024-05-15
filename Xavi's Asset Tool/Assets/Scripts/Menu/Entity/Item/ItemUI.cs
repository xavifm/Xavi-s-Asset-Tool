using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ItemUI : MonoBehaviour
{
    public string Name;
    public Text Description;
    public Entity Entity;
    public int Quantity = 1;

    [SerializeField] TextMeshProUGUI NameText;
    [SerializeField] TextMeshProUGUI QuantityText;

    bool ItemStarted = false;

    public void Update()
    {
        if(!ItemStarted)
            StartCoroutine(InitializeItem());
    }

    public IEnumerator InitializeItem()
    {
        yield return new WaitForSeconds(0.01f);
        NameText.text = Name;
        QuantityText.text = Quantity.ToString();

        ItemStarted = true;
    }

    public void UpdateItem(Entity _object)
    {
        Name = _object.Name;
        Description = _object.Description;
    }
}
