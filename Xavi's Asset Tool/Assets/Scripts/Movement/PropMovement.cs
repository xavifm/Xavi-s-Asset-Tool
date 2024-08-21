using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropMovement : Movement
{
    enum BreakStyle { BREAK, EXPLOSION, WALL_BREAK }
    [SerializeField] bool Breakable;
    [SerializeField] float BreakVelocity;
    [SerializeField] float ExplosionRadius;
    [SerializeField] float ExplosionForce;
    [SerializeField] BreakStyle BreakType;
    [SerializeField] float DestroyTime;

    public override void MovementLogic()
    {
        base.MovementLogic();
        DestroyLogic();
    }

    private void DestroyLogic(Collision _collision = null)
    {
        if (Breakable)
        {
            if (ColliderEntity != null)
            {
                Entity.EntityType typeOfEntity = Entity.EntityType.DEFAULT;
                Entity entityQuery = GetObjectEntity(ColliderEntity);
                if (entityQuery != null)
                    typeOfEntity = entityQuery.TypeOfEntity;

                if (typeOfEntity != Entity.EntityType.CREATURE_PLAYER)
                {
                    if (EntityRb.velocity.magnitude >= BreakVelocity
                        || (entityQuery != null
                            && entityQuery.MovementLogic.EntityRb.velocity.magnitude >= BreakVelocity
                            && BreakType.Equals(BreakStyle.WALL_BREAK)))
                        ExecuteDestruction(BreakType, _collision);
                }
            }
        }
    }

    private void ExecuteDestruction(BreakStyle _style, Collision _collision = null)
    {
        if (BreakType.Equals(BreakStyle.EXPLOSION))
            Explode(EntityRb.velocity, ExplosionRadius, ExplosionForce);

        if (BreakType.Equals(BreakStyle.BREAK))
            Destroy(DestroyTime);

        if (_collision != null && BreakType.Equals(BreakStyle.WALL_BREAK))
            FragmentEntity.FragmentByCollision(_collision);         
    }

    public override void OnCollisionStay(Collision collision)
    {
        base.OnCollisionStay(collision);
        DestroyLogic(collision);
    }

}
