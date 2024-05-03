using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesMenuManager : MonoBehaviour
{
    [SerializeField] Transform PivotParent;
    [SerializeField] GameObject ItemPrefab;

    public void UpdateEntityList(List<GameObject> Items)
    {
        ResetList();
        PopulateList(Items);
    }

    void ResetList()
    {
        foreach (Transform child in PivotParent)
        {
            Destroy(child.gameObject);
        }
    }

    void PopulateList(List<GameObject> Items)
    {
        foreach (GameObject item in Items)
        {
            Entity entity = item.GetComponent<Entity>();
            if (entity != null)
            {
                GameObject newItem = Instantiate(ItemPrefab, PivotParent);
                ItemUI itemUI = newItem.GetComponent<ItemUI>();

                if (itemUI != null)
                {
                    itemUI.Name = entity.Name;
                    itemUI.Description = entity.Description;
                }
            }
            else
            {
                Debug.LogWarning("GameObject " + item.name + " does not have Entity attached.");
            }
        }
    }
}
