using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<GameObject> Items;
    [SerializeField] List<Entity.EntityType> StorageTypesList;

    const float STORE_SIZE = 0.5f;

    public bool StoreItem(Entity _object)
    {
        if(CheckIfIsForStorage(_object))
        {
            Items.Add(_object.gameObject);
            StartCoroutine(StoreCoroutine(_object));
        }

        return false;
    }

    public bool RetrieveItem(Entity _object)
    {
        if (CheckIfItemExists(_object))
        {
            Items.Remove(_object.gameObject);
            StartCoroutine(RetrieveCoroutine(_object));
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

    private IEnumerator StoreCoroutine(Entity _object)
    {
        while(_object.transform.localScale.magnitude > STORE_SIZE)
        {
            _object.MovementLogic.Resize(Vector3.zero, true);
        }

        yield return null;
    } 

    private IEnumerator RetrieveCoroutine(Entity _object)
    {
        float originalMagnitude = _object.MovementLogic.ResizeEntity.OriginalSize.magnitude;

        while (_object.transform.localScale.magnitude < originalMagnitude)
        {
            _object.MovementLogic.RestoreSize(true);
        }

        yield return null;
    }
}
