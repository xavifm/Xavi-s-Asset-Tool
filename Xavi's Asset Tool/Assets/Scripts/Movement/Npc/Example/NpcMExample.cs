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

        if(Vector3.Distance(Player.position, transform.position) >= 2)
        {
            Vector3 dir = (Player.position - transform.position);
            dir = new Vector3(dir.x, 0, dir.z);

            EntityRb.velocity = dir.normalized * Velocity;
        }
    }

    public override void RotationLogic()
    {
        base.RotationLogic();

        Quaternion lookQuaternion = GetQuaternionLookingAt(Player.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookQuaternion, Velocity * Time.deltaTime);
    }

    private Quaternion GetQuaternionLookingAt(Vector3 _position)
    {
        Vector3 direction = (_position - transform.position).normalized;
        Quaternion finalRotation = Quaternion.LookRotation(direction);

        return Quaternion.Euler(0, finalRotation.eulerAngles.y, 0);
    }
}
