using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMovement : Movement
{
    [SerializeField] protected float Velocity;
    [SerializeField] protected float ResetSpeed;
    [SerializeField] protected float JumpForce;

    public virtual void JumpLogic()
    {

    }
}
