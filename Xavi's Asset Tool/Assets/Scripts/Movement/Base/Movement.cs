using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody EntityRb;
    public Animator CreatureAnimator;

    [SerializeField] protected float Velocity;
    [SerializeField] protected float ResetSpeed;
    [SerializeField] protected float JumpForce;

    protected string CollisionTag;
    protected bool Colliding;

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

    public virtual void JumpLogic()
    {

    }

    protected bool CheckCollisionWithTag(string _tag)
    {
        if(!CollisionTag.Equals(string.Empty) && Colliding && CollisionTag.Equals(_tag))
            return true;

        return false;
    }

    private void OnCollisionStay(Collision collision)
    {
        CollisionTag = collision.gameObject.tag;
        Colliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        CollisionTag = "";
        Colliding = true;
    }
}
