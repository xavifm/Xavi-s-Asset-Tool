using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMExample : NpcMovementEntity
{
    [SerializeField] Transform Target;
    [SerializeField] float TargetFindDistance;
    [SerializeField] float StopDistance = 2;

    //ultra simple dirty exampled, remake it clear by using entity component system integrated

    public override void MovementLogic()
    {
        base.MovementLogic();

        Target = GetClosestTarget(TargetFindDistance, Entity.EntityType.CREATURE_PLAYER);

        if (Target != null
            && Vector3.Distance(Target.position, transform.position) >= StopDistance
            && CurrentAnimation != AnimationStates.DAMAGE && CurrentAnimation != AnimationStates.DEAD)
        {
            Vector3 direction = (Target.position - transform.position);
            direction = new Vector3(direction.x, 0, direction.z);

            EntityRb.velocity = direction.normalized * Velocity;
        }
    }

    public override void RotationLogic()
    {
        base.RotationLogic();

        if (Target == null)
            return;

        Quaternion lookQuaternion = GetQuaternionLookingAt(Target.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookQuaternion, RotationSpeed * Time.deltaTime);
    }

    private Quaternion GetQuaternionLookingAt(Vector3 _position)
    {
        Vector3 direction = (_position - transform.position).normalized;
        Quaternion finalRotation = Quaternion.LookRotation(direction);

        return Quaternion.Euler(0, finalRotation.eulerAngles.y, 0);
    }

}
