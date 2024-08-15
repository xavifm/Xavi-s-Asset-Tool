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
    [SerializeField] protected float DamageEntitySpeed = 0.5f;

    private const float DIVISION_MARGIN = 1.05f;

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

    public virtual void CollisionLogic()
    {
        if (ColliderEntity == null)
            return;

        Entity entityQuery = GetObjectEntity(ColliderEntity);

        if(entityQuery != null)
        {
            Movement movement = entityQuery.MovementLogic;

            float rigidbodyVelocity = movement.EntityRb.velocity.magnitude;

            if (entityQuery.TypeOfEntity.Equals(Entity.EntityType.WORLD_PROP) && rigidbodyVelocity > DamageEntitySpeed)
            {
                Vector3 hitVector = transform.TransformDirection(Vector3.back) * (rigidbodyVelocity / (rigidbodyVelocity / DIVISION_MARGIN));
                EntityRb.velocity = EntityRb.velocity + hitVector;
                DamageEntity();
            }
        }
    }

    public virtual void JumpLogic()
    {

    }

}
