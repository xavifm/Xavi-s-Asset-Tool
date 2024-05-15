using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesMenuManager : MonoBehaviour
{
    [SerializeField] Transform PivotParent;
    [SerializeField] GameObject ItemPrefab;
    [SerializeField] LegendDetail LegendDetailMenu;

    public void UpdateEntityList(List<GameObject> Items)
    {
        StartCoroutine(UpdateEntityListCoroutine(Items));
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
            Entity UIQuery = GetExistingEntity(entity);


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
                IncrementUIQuantity(UIQuery);
        }
    }

    void IncrementUIQuantity(Entity _entity)
    {
        Transform queryUI = GetUIItem(_entity);

        if (queryUI != null)
            queryUI.GetComponent<ItemUI>().Quantity += 1;
    }

    Entity GetExistingEntity(Entity _entity)
    {
        Transform query = GetUIItem(_entity);
        Entity result = null;

        if(query != null)
            result = query.GetComponent<ItemUI>().Entity;

        return result;
    }

    Transform GetUIItem(Entity _entity)
    {
        Transform query = null;

        foreach (Transform child in PivotParent)
        {
            ItemUI details = child.GetComponent<ItemUI>();
            Entity entity = details.Entity;

            if (details != null && CheckIfAreSameIdentity(_entity, entity))
                query = child;

        }

        return query;
    }

    bool CheckIfAreSameIdentity(Entity _entity1, Entity _entity2)
    {
        bool result = false;

        if (_entity1.Name.Equals(_entity2.Name) &&
            _entity1.Description.text.Equals(_entity2.Description.text) &&
            _entity1.TypeOfEntity.Equals(_entity2.TypeOfEntity))
            result = true;

        return result;
    }

    IEnumerator UpdateEntityListCoroutine(List<GameObject> Items)
    {
        ResetList();
        yield return new WaitForSeconds(0.05f);
        PopulateList(Items);
    }
}
