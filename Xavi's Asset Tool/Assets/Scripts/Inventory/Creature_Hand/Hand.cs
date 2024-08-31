using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Entity CurrentHandItem;
    public Entity.EntityType[] FastEquipableItems;
    
    [SerializeField] Transform ThrowPivot;
    [SerializeField] float ThrowForce = 2;
    

    const float THROW_MARGIN = 0.8f;
    const float WEAPON_HAND_MARGIN = 1.5f;

    public void SetHandItem(Entity _item)
    {
        if (_item != null && !_item.StoredItem)
            return;

        if (CurrentHandItem != null)
            HideCurrentItem();

        if(_item != null)
        {
            Transform itemTransform = _item.transform;

            itemTransform.parent = transform;
            itemTransform.localPosition = Vector3.zero;
            itemTransform.localRotation = Quaternion.identity;

            if (_item.TypeOfEntity.Equals(Entity.EntityType.WEAPON))
                itemTransform.localPosition += new Vector3(WEAPON_HAND_MARGIN, 0, 0);

            CurrentHandItem = _item;

            itemTransform.gameObject.SetActive(true);
            _item.MovementLogic.DisablePhysics();
        }
    }

    public void DropHandItem(Entity _item)
    {
        if (CurrentHandItem != null && _item.Equals(CurrentHandItem))
            DropCurrentHandItem();
    }

    public void ThrowHandItem(Inventory _inventory)
    {
        if(CurrentHandItem != null)
        {
            Entity throwObject = CurrentHandItem;
            DropCurrentHandItem();
            _inventory.RetrieveItem(throwObject);

            throwObject.transform.position = ThrowPivot.transform.position + ThrowPivot.TransformDirection(Vector3.forward) * THROW_MARGIN;
            throwObject.MovementLogic.EntityRb.velocity = ThrowPivot.TransformDirection(Vector3.forward) * ThrowForce;
        }
    }

    public void InteractWithHandEntity(Entity _entity)
    {
        _entity.InteractWith();
    }

    public void DropCurrentHandItem()
    {
        if (CurrentHandItem != null)
        {
            GameObject currentHandItemObject = CurrentHandItem.gameObject;
            currentHandItemObject.transform.parent = null;
            CurrentHandItem.MovementLogic.EnablePhysics();
            currentHandItemObject.SetActive(false);
            CurrentHandItem = null;
        }
    }

    private void HideCurrentItem()
    {
        GameObject currentHandItemObject = CurrentHandItem.gameObject;
        currentHandItemObject.transform.parent = null;
        currentHandItemObject.SetActive(false);
    }
}
