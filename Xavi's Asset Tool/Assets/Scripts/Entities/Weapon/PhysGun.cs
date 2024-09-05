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

    private WeaponMovement WeaponMovementAux;
    private Entity CatchedProp;
    private bool CatchedItem;
    private int CloseKeyTimesPressed;

    private const float BaseCooldown = 0.2f;
    private const float ThrowTimeAnimation = 0.5f;
    private float CooldownTimer;

    public override void VirtualUpdate()
    {
        base.VirtualUpdate();
        UpdateCooldown();

        if (WeaponMovementAux == null)
            WeaponMovementAux = (WeaponMovement) MovementLogic;

        AnimationLogicFromEntity();

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

    private void AnimationLogicFromEntity()
    {
        if (CreatureRb != null)
            WeaponMovementAux.AnimationLogic(CreatureRb.velocity.magnitude);

        if (StoredItem && WeaponMovementAux.CurrentAnimation.Equals(WeaponMovement.AnimationStates.FLOOR))
            WeaponMovementAux.SwitchState(WeaponMovement.AnimationStates.IDLE);
        if (!StoredItem && WeaponMovementAux.CurrentAnimation.Equals(WeaponMovement.AnimationStates.IDLE))
            WeaponMovementAux.SwitchState(WeaponMovement.AnimationStates.FLOOR);

        if (Input.GetKeyUp(CloseKey) && !CatchedItem)
            WeaponMovementAux.SwitchState(WeaponMovement.AnimationStates.IDLE);
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

        if (!CatchedItem)
            WeaponMovementAux.SwitchState(WeaponMovement.AnimationStates.PHYSGUN_GET);

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
            WeaponMovementAux.SwitchState(WeaponMovement.AnimationStates.IDLE);

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

        ReleaseCatchedProp(true);
    }

    private void ReleaseCatchedProp(bool _playThrowAnimation = false)
    {
        if(_playThrowAnimation)
            StartCoroutine(ReleaseAnimation());

        CatchedItem = false;
        CatchedProp = null;
    }

    private IEnumerator ReleaseAnimation()
    {
        WeaponMovementAux.SwitchState(WeaponMovement.AnimationStates.PGYSGUN_THROW);

        yield return new WaitForSeconds(ThrowTimeAnimation);

        WeaponMovementAux.SwitchState(WeaponMovement.AnimationStates.IDLE);
    }
}



