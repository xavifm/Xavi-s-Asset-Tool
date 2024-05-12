using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<GameObject> Items;
    [SerializeField] List<Entity.EntityType> StorageTypesList;

    [SerializeField] EntitiesMenuManager MenuView;

    const float STORE_SIZE = 0.5f;

    public void StoreItem(Entity _object)
    {
        if(!_object.StoredItem && CheckIfIsForStorage(_object))
        {
            Items.Add(_object.gameObject);
            _object.StoredItem = true;

            StartCoroutine(StoreCoroutine(_object));

            if(MenuView != null)
                MenuView.UpdateEntityList(Items);
        }
    }

    public void RetrieveItem(Entity _object)
    {
        GameObject query = CheckIfItemExists(_object);

        if (query != null)
        {
            Items.Remove(query);
            _object.StoredItem = false;

            Entity entity = query.GetComponent<Entity>();
            RetrieveLogic(entity);

            if (MenuView != null)
                MenuView.UpdateEntityList(Items);
        }
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
        GameObject sceneObject = _object.gameObject;
        sceneObject.transform.position = transform.position;
        sceneObject.SetActive(true);

        _object.MovementLogic.RestoreSize();
    }
}
