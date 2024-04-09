using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMovement : Movement
{
    [SerializeField] protected float Velocity;
    [SerializeField] protected float ResetSpeed;
    [SerializeField] protected float JumpForce;
    [SerializeField] protected Transform RaycastReferencePoint;
    [SerializeField] protected float PickupDistance = 5;

    public override void MovementLogic()
    {
        base.MovementLogic();

        Vector3 resetVector = new Vector3(0, EntityRb.velocity.y, 0);
        EntityRb.velocity = Vector3.Lerp(EntityRb.velocity, resetVector, Time.deltaTime * ResetSpeed);
    }

    public virtual Entity CheckPointingEntity()
    {
        Entity objectExtracted = null;

        if (!RaycastReferencePoint.Equals(null))
        {
            RaycastHit hit;

            if (Physics.Raycast(RaycastReferencePoint.position, RaycastReferencePoint.TransformDirection(Vector3.forward), out hit, PickupDistance))
                objectExtracted = hit.collider.GetComponent<Entity>();
        }

        return objectExtracted;
    }

    public virtual void JumpLogic()
    {

    }

}
