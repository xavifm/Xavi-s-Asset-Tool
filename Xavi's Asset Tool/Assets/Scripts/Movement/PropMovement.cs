using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropMovement : Movement
{
    enum BreakStyle { BREAK, EXPLOSION }
    [SerializeField] bool Breakable;
    [SerializeField] float BreakVelocity;
    [SerializeField] BreakStyle BreakType;
    [SerializeField] float DestroyTime;

    public override void MovementLogic()
    {
        DestroyLogic();
    }

    private void DestroyLogic()
    {
        if (Breakable)
        {
            if (EntityRb.velocity.magnitude >= BreakVelocity && ColliderEntity != null)
            {
                Entity.EntityType typeOfEntity = Entity.EntityType.DEFAULT;
                Entity entityQuery = GetObjectEntity(ColliderEntity.transform);
                if (entityQuery != null)
                    typeOfEntity = entityQuery.TypeOfEntity;

                if (typeOfEntity != Entity.EntityType.CREATURE_PLAYER)
                {
                    ExecuteDestruction(BreakType);
                }
            }
        }
    }

    private void ExecuteDestruction(BreakStyle _style)
    {
        float velocityMagnitude = EntityRb.velocity.magnitude;

        if (BreakType.Equals(BreakStyle.EXPLOSION))
            Explode(EntityRb.velocity, velocityMagnitude, velocityMagnitude);

        if (BreakType.Equals(BreakStyle.BREAK))
            Destroy(DestroyTime);
    }

    private Entity GetObjectEntity(Transform _object)
    {
        Entity entityQuery = null;

        if (_object.parent != null)
            entityQuery = _object.parent.GetComponent<Entity>();
        else
            entityQuery = _object.GetComponent<Entity>();

        return entityQuery;
    }
}
