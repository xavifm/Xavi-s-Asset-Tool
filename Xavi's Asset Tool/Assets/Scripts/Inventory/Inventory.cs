using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> Items;
    [SerializeField] List<Entity.EntityType> StorageTypesList;

    [SerializeField] EntitiesMenuManager MenuView;

    const float STORE_SIZE = 0.5f;

    public void StoreItem(Entity _object, Entity _creatureEntity)
    {
        if(!_object.StoredItem && CheckIfIsForStorage(_object))
        {
            Items.Add(_object.gameObject);
            _object.StoredItem = true;

            if (_object.TypeOfEntity.Equals(Entity.EntityType.WEAPON))
                StoreWeaponLogic(_object, _creatureEntity);

            StartCoroutine(StoreCoroutine(_object));

            if(MenuView != null)
                MenuView.UpdateEntityList(this);
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
                MenuView.UpdateEntityList(this);
        }
    }

    public Entity GetNextWeapon(Entity _currentObject, Entity.EntityType[] _availableTypes , float _direction)
    {
        Entity itemQuery = null;

        int currentIndex = GetItemIndexFromInventory(_currentObject);

        if (currentIndex == -1)
            return GetAnyAvailableEntity(_availableTypes);

        int nextIndex = currentIndex;

        do
        {
            nextIndex += (int)_direction;

            if (nextIndex < 0)
                nextIndex = Items.Count - 1;
            else if (nextIndex >= Items.Count)
                nextIndex = 0;

            Entity nextEntity = Items[nextIndex].GetComponent<Entity>();

            if (nextEntity != null 
                && (!CheckIfAreSameIdentity(_currentObject, nextEntity) || nextEntity.Equals(_currentObject)) 
                && System.Array.Exists(_availableTypes, type => type == nextEntity.TypeOfEntity))
            {
                itemQuery = nextEntity;
                break;
            }
        } 
        while (nextIndex != currentIndex);

        return itemQuery;
    }

    public Entity GetItemByIdentity(Entity _object)
    {
        Entity itemQuery = null;

        foreach (GameObject item in Items)
        {
            Entity entity = item.GetComponent<Entity>();
            if (CheckIfAreSameIdentity(entity, _object))
                itemQuery = entity;
        }

        return itemQuery;
    }

    public bool CheckIfAreSameIdentity(Entity _entity1, Entity _entity2)
    {
        bool result = false;

        if (_entity1 == null || _entity2 == null)
            return result;

        if (_entity1.Name.Equals(_entity2.Name) &&
            _entity1.Description.text.Equals(_entity2.Description.text) &&
            _entity1.TypeOfEntity.Equals(_entity2.TypeOfEntity))
            result = true;

        return result;
    }

    private void StoreWeaponLogic(Entity _object, Entity _creatureEntity)
    {
        Weapon weaponStored = (Weapon) _object;
        CreatureMovement movementCreature = (CreatureMovement)_creatureEntity.MovementLogic;
        weaponStored.RayCastPoint = movementCreature.RaycastReferencePoint;
    }

    private Entity GetAnyAvailableEntity(Entity.EntityType[] _availableTypes)
    {
        Entity entityQuery = null;

        foreach (GameObject item in Items)
        {
            Entity entity = item.GetComponent<Entity>();
            if (System.Array.Exists(_availableTypes, type => type == entity.TypeOfEntity))
            {
                entityQuery = entity;
                break;
            }
        }

        return entityQuery;
    }

    private int GetItemIndexFromInventory(Entity _currentObject)
    {
        int currentIndex = -1;

        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].GetComponent<Entity>() == _currentObject)
            {
                currentIndex = i;
                break;
            }
        }

        return currentIndex;
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

        _object.MovementLogic.ResetCollisionInfo();
        _object.gameObject.SetActive(false);
    } 

    private void RetrieveLogic(Entity _object)
    {
        GameObject sceneObject = _object.gameObject;
        sceneObject.transform.position = transform.position;
        sceneObject.SetActive(true);

        _object.MovementLogic.RestoreSize();

        if(_object.TypeOfEntity.Equals(Entity.EntityType.WEAPON))
            _object.MovementLogic.RestoreSize(_modelResize: true);
    }
}
