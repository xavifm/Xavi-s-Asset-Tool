using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesMenuManager : MonoBehaviour
{
    [SerializeField] Transform PivotParent;
    [SerializeField] GameObject ItemPrefab;
    [SerializeField] LegendDetail LegendDetailMenu;

    public void UpdateEntityList(Inventory _inventory)
    {
        StartCoroutine(UpdateEntityListCoroutine(_inventory));
    }

    void ResetList()
    {
        foreach (Transform child in PivotParent)
        {
            Destroy(child.gameObject);
        }
    }

    void PopulateList(Inventory _inventory)
    {
        foreach (GameObject item in _inventory.Items)
        {
            Entity entity = item.GetComponent<Entity>();
            Entity UIQuery = GetExistingEntity(entity, _inventory);


            if (UIQuery == null && entity != null)
            {
                GameObject newItem = Instantiate(ItemPrefab, PivotParent);
                ItemUI itemUI = newItem.GetComponent<ItemUI>();

                if (itemUI != null)
                {
                    itemUI.UpdateItem(entity);

                    UIDetails details = newItem.GetComponent<UIDetails>();
                    details.Legend = LegendDetailMenu;
                    itemUI.Entity = entity;
                }
            }
            else if (UIQuery != null)
                IncrementUIQuantity(UIQuery, _inventory);
        }
    }

    void IncrementUIQuantity(Entity _entity, Inventory _inventory)
    {
        Transform queryUI = GetUIItem(_entity, _inventory);

        if (queryUI != null)
            queryUI.GetComponent<ItemUI>().Quantity += 1;
    }

    Entity GetExistingEntity(Entity _entity, Inventory _inventory)
    {
        Transform query = GetUIItem(_entity, _inventory);
        Entity result = null;

        if(query != null)
            result = query.GetComponent<ItemUI>().Entity;

        return result;
    }

    Transform GetUIItem(Entity _entity, Inventory _inventory)
    {
        Transform query = null;

        foreach (Transform child in PivotParent)
        {
            ItemUI details = child.GetComponent<ItemUI>();
            Entity entity = details.Entity;

            if (details != null && _inventory.CheckIfAreSameIdentity(_entity, entity))
                query = child;

        }

        return query;
    }

    IEnumerator UpdateEntityListCoroutine(Inventory _inventory)
    {
        ResetList();
        yield return new WaitForSeconds(0.05f);
        PopulateList(_inventory);
    }
}
