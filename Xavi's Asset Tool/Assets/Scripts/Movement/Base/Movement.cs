using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody EntityRb;
    public Animator CreatureAnimator;

    [SerializeField] protected float Velocity;
    [SerializeField] protected float ResetSpeed;

    public virtual void MovementStateMachine()
    {

    }

    public virtual void MovementLogic()
    {
        Vector3 currentVelocity = Vector3.zero;
    }

    public virtual void RotationLogic()
    {

    }
}
