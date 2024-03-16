using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody EntityRb;
    public Animator EntityAnimator;

    [HideInInspector] public string CollisionTag;
    [HideInInspector] public bool Colliding;

    public virtual void AnimationStateMachine()
    {

    }

    public virtual void MovementLogic()
    {

    }

    public virtual void RotationLogic()
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
