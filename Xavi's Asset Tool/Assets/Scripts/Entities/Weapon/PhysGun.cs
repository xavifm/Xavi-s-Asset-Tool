using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysGun : Weapon
{
    [SerializeField] private KeyCode AttackKey;
    [SerializeField] private KeyCode CloseKey;
    [SerializeField] private float CatchDistance;
    [SerializeField] private float ObtainedItemPhysGunDistance;
    [SerializeField] private float DistanceWeaponCatchOffset;
    [SerializeField] private float CatchForce;
    [SerializeField] private float ThrowForce;
    [SerializeField] private float CaughtLerpVelocity;

    private Entity CatchedProp;
    private bool CatchedItem;
    private int CloseKeyTimesPressed;

    private const float BaseCooldown = 0.2f;
    private float CooldownTimer;

    public override void VirtualUpdate()
    {
        base.VirtualUpdate();
        UpdateCooldown();

        if (CatchedItem && CatchedProp?.MovementLogic != null)
        {
            HandleCloseKeyPress();
            UpdateEntityPosition();
        }
    }

    public override void InteractWithWeapon(KeyCode _key)
    {
        base.InteractWithWeapon(_key);
        HandlePhysGunLogic(_key);
    }

    private void UpdateCooldown()
    {
        if (CooldownTimer > 0)
            CooldownTimer -= Time.deltaTime;
    }

    private bool IsCooldownActive() => CooldownTimer > 0;

    private void HandlePhysGunLogic(KeyCode _key)
    {
        if (IsCooldownActive()) return;

        Entity forwardEntity = DetectEntityInFront();

        if (_key == CloseKey)
        {
            if (!CatchedItem && forwardEntity != null)
            {
                CatchEntity(forwardEntity);
            }
        }
        else if (_key == AttackKey && CatchedItem)
        {
            ThrowCatchedProp();
        }
    }

    private Entity DetectEntityInFront() =>
        MovementLogic.CheckPointingEntity(RayCastPoint, CatchDistance);

    private void CatchEntity(Entity _entity)
    {
        CatchedProp = _entity;
        ApplyCatchForce();

        if (IsWithinCatchDistance())
        {
            CatchedItem = true;
            CloseKeyTimesPressed = 1;
        }
    }

    private bool IsWithinCatchDistance()
    {
        return Vector3.Distance(CatchedProp.MovementLogic.EntityTransform.position, transform.position) <= ObtainedItemPhysGunDistance;
    }

    private void ApplyCatchForce()
    {
        if (CatchedProp?.MovementLogic == null) return;

        Vector3 direction = (transform.position - CatchedProp.MovementLogic.EntityTransform.position).normalized;
        CatchedProp.MovementLogic.EntityRb.velocity = direction * CatchForce;
    }

    private void HandleCloseKeyPress()
    {
        if (Input.GetKeyDown(CloseKey))
        {
            CloseKeyTimesPressed++;
            ReleaseEntityOnSecondPress();
        }
    }

    private void ReleaseEntityOnSecondPress()
    {
        if (CloseKeyTimesPressed >= 2)
        {
            CooldownTimer = BaseCooldown;
            ReleaseCatchedProp();
            CloseKeyTimesPressed = 0;
        }
    }

    private void UpdateEntityPosition()
    {
        if (CatchedProp == null) return;

        Vector3 offset = RayCastPoint.TransformDirection(Vector3.forward) * DistanceWeaponCatchOffset;
        Vector3 targetPosition = RayCastPoint.position + offset;

        CatchedProp.MovementLogic.EntityRb.velocity = Vector3.zero;
        CatchedProp.transform.position = Vector3.Lerp(
            CatchedProp.MovementLogic.EntityTransform.position,
            targetPosition,
            Time.deltaTime * CaughtLerpVelocity
        );

        HandleEntityDistance();
    }

    private void HandleEntityDistance()
    {
        if (CatchedProp == null) return;

        float distanceToGun = Vector3.Distance(CatchedProp.MovementLogic.EntityTransform.position, transform.position);

        if (!CatchedItem && distanceToGun <= ObtainedItemPhysGunDistance)
        {
            CatchedItem = true;
        }
        else if (distanceToGun > ObtainedItemPhysGunDistance)
        {
            ReleaseCatchedProp();
        }
    }

    private void ThrowCatchedProp()
    {
        if (CatchedProp?.MovementLogic == null) return;

        CatchedProp.MovementLogic.EntityRb.velocity =
            RayCastPoint.TransformDirection(Vector3.forward) * ThrowForce;

        ReleaseCatchedProp();
    }

    private void ReleaseCatchedProp()
    {
        CatchedItem = false;
        CatchedProp = null;
    }
}



