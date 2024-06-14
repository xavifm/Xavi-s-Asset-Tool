using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropMovement : Movement
{
    [SerializeField] bool Breakable;
    [SerializeField] float BreakVelocity;

    public override void MovementLogic()
    {
        if(Breakable)
        {
            if(EntityRb.velocity.magnitude >= BreakVelocity && ColliderEntity != null)
            {
                Entity.EntityType typeOfEntity = Entity.EntityType.DEFAULT;
                Entity entityQuery = GetObjectEntity(ColliderEntity.transform);
                if (entityQuery != null)
                    typeOfEntity = entityQuery.TypeOfEntity;
                
                if(typeOfEntity != Entity.EntityType.CREATURE_PLAYER)
                {
                    float velocityMagnitude = EntityRb.velocity.magnitude;
                    Explode(Vector3.zero, velocityMagnitude / 2, velocityMagnitude);
                }
            }
        }
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
