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

        Vector3 dir = (Player.position - transform.position).normalized;
        Vector3 forward = Vector3.forward;
        Vector3 axis = Vector3.Cross(forward, dir).normalized;
        float dot = Vector3.Dot(forward, dir);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, axis);
        rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Velocity * Time.deltaTime);
    }
}
