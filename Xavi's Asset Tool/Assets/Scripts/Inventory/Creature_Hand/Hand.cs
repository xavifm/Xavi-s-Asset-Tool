using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] Entity CurrentHandItem;

    public void SetHandItem(Entity _item)
    {

        if (CurrentHandItem != null)
            HideCurrentItem();

        if(_item != null)
        {
            Transform itemTransform = _item.transform;

            itemTransform.parent = transform;
            itemTransform.localPosition = Vector3.zero;
            itemTransform.localRotation = Quaternion.identity;

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
