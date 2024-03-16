using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMovement : Movement
{
    [SerializeField] protected float Velocity;
    [SerializeField] protected float ResetSpeed;
    [SerializeField] protected float JumpForce;

    public override void MovementLogic()
    {
        base.MovementLogic();

        Vector3 resetVector = new Vector3(0, EntityRb.velocity.y, 0);
        EntityRb.velocity = Vector3.Lerp(EntityRb.velocity, resetVector, Time.deltaTime * ResetSpeed);
    }

    public virtual void JumpLogic()
    {

    }
}
