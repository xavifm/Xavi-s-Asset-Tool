using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<GameObject> Items;
    [SerializeField] List<Entity.EntityType> StorageTypesList;

    public bool StoreItem(Entity _object)
    {
        if(CheckIfIsForStorage(_object))
        {
            Items.Add(_object.gameObject);

            //more future logic for storing items
        }

        return false;
    }

    public bool RetrieveItem(Entity _object)
    {
        if (CheckIfItemExists(_object))
        {
            //more future logic for retrieving items

            Items.Remove(_object.gameObject);
        }

        return false;
    }

    private bool CheckIfItemExists(Entity _object)
    {
        foreach (GameObject item in Items)
        {
            if (item.gameObject.Equals(_object.TypeOfEntity))
                return true;
        }

        return false;
    }

    private bool CheckIfIsForStorage(Entity _object)
    {
        foreach(Entity.EntityType item in StorageTypesList)
        {
            if (item.Equals(_object.TypeOfEntity))
                return true;
        }

        return false;
    }
}
