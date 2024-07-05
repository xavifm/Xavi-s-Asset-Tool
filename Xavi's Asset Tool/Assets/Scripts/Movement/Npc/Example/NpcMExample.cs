using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMExample : NpcMovementEntity
{
    public Transform Player;

    //ultra simple dirty exampled, remake it clear by using entity component system integrated

    public override void MovementLogic()
    {
        base.MovementLogic();

        if(Vector3.Distance(Player.position, transform.position) >= 2 
            && CurrentAnimation != AnimationStates.DAMAGE)
        {
            Vector3 direction = (Player.position - transform.position);
            direction = new Vector3(direction.x, 0, direction.z);

            EntityRb.velocity = direction.normalized * Velocity;
        }
    }

    public override void RotationLogic()
    {
        base.RotationLogic();

        Quaternion lookQuaternion = GetQuaternionLookingAt(Player.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookQuaternion, RotationSpeed * Time.deltaTime);
    }

    private Quaternion GetQuaternionLookingAt(Vector3 _position)
    {
        Vector3 direction = (_position - transform.position).normalized;
        Quaternion finalRotation = Quaternion.LookRotation(direction);

        return Quaternion.Euler(0, finalRotation.eulerAngles.y, 0);
    }

}
