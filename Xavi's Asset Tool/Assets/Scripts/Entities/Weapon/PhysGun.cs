using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysGun : Weapon
{
    [SerializeField] KeyCode AttackKey;
    [SerializeField] KeyCode CloseKey;
    [SerializeField] float CatchDistance;
    [SerializeField] float ObtainedItemPhysGunDistance;
    [SerializeField] float DistanceWeaponCatchOffset;
    [SerializeField] float CatchForce;
    [SerializeField] float ThrowForce;
    [SerializeField] float CaughtLerpVelocity;
    Entity CatchedProp;

    int CloseKeyTimesPressed;
    bool CatchedItem;

    const float BaseCoolDownTimer = 0.2f;
    float CoolDownTimer = 0;

    public override void VirtualUpdate()
    {
        base.VirtualUpdate();

        if (CoolDownTimer > 0)
            CoolDownTimer -= Time.deltaTime;

        if (CatchedItem && CatchedProp != null && CatchedProp.MovementLogic != null)
        {
            if (Input.GetKeyDown(CloseKey))
                CloseKeyTimesPressed++;

            if (CloseKeyTimesPressed >= 2)
            {
                CoolDownTimer = BaseCoolDownTimer;
                ReleaseItemFromWeapon();
                CloseKeyTimesPressed = 0;
            }

            Vector3 vectorOffset = RayCastPoint.TransformDirection(Vector3.forward) * DistanceWeaponCatchOffset;
            Vector3 finalVector = RayCastPoint.position + vectorOffset;

            if(CatchedProp != null)
            {
                CatchedProp.MovementLogic.EntityRb.velocity = Vector3.zero;
                CatchedProp.transform.position = Vector3.Lerp(CatchedProp.MovementLogic.EntityTransform.position, finalVector, Time.deltaTime * CaughtLerpVelocity);
            }
 
        }
    }

    public override void InteractWithWeapon(KeyCode _key)
    {
        base.InteractWithWeapon(_key);
        PhysGunLogic(_key);
    }

    private void PhysGunLogic(KeyCode _key)
    {
        Entity forwardEntity = MovementLogic.CheckPointingEntity(RayCastPoint, CatchDistance);
        if(forwardEntity != null)
            Debug.Log(forwardEntity.gameObject.name);

        if (CoolDownTimer > 0)
            return;

        if (forwardEntity != null && !CatchedItem && _key == CloseKey)
        {
            CatchedProp = forwardEntity;
        }

        if(CatchedProp != null)
        {
            if(_key == CloseKey && !CatchedItem && CatchedProp.MovementLogic != null)
                CatchedProp.MovementLogic.EntityRb.velocity = (transform.position - CatchedProp.MovementLogic.EntityTransform.position).normalized * CatchForce;

            float distanceBetweenGun = Vector3.Distance(CatchedProp.MovementLogic.EntityTransform.position, transform.position);

            if (distanceBetweenGun <= ObtainedItemPhysGunDistance && !CatchedItem)
                CatchItemWithPhysGun();

            if (_key != CloseKey && distanceBetweenGun > ObtainedItemPhysGunDistance)
                ReleaseItemFromWeapon();

            if(_key == AttackKey && CatchedProp.MovementLogic != null)
            {
                CatchedProp.MovementLogic.EntityRb.velocity = RayCastPoint.TransformDirection(Vector3.forward) * ThrowForce;
                ReleaseItemFromWeapon();
            }
        }
    }

    private void CatchItemWithPhysGun()
    {
        CatchedItem = true;
        CloseKeyTimesPressed += 1;
    }

    private void ReleaseItemFromWeapon()
    {
        CatchedItem = false;
        CatchedProp = null;
    }
}
