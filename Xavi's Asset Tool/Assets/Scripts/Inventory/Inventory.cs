using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<GameObject> Items;
    [SerializeField] List<Entity.EntityType> StorageTypesList;

    /* DEBUG */ public Entity test;
    /* DEBUG */ public bool keep;

    const float STORE_SIZE = 0.5f;

    /* DEBUG */
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            keep = !keep;
            if (keep)
                StoreItem(test);
            else
                RetrieveItem(test);
        }
    }

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
        GameObject query = CheckIfItemExists(_object);

        if (query != null)
        {
            Items.Remove(query);
            Entity entity = query.GetComponent<Entity>();
            RetrieveLogic(entity);
        }

        return false;
    }

    private GameObject CheckIfItemExists(Entity _object)
    {
        foreach (GameObject item in Items)
        {
            if (item.gameObject.Equals(_object.gameObject))
                return item;
        }

        return null;
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
            yield return null;
        }

        _object.gameObject.SetActive(false);
    } 

    private void RetrieveLogic(Entity _object)
    {
        _object.gameObject.SetActive(true);
        _object.MovementLogic.RestoreSize();
    }
}
