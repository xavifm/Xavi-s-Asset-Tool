using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody EntityRb;
    public Transform EntityTransform;
    public Animator EntityAnimator;
    
    [SerializeField] EntityResize ResizeEntity;
    [SerializeField] float ResizeSpeed = 2;

    [HideInInspector] public string CollisionTag;
    [HideInInspector] public bool Colliding;


    void Start()
    {
        EntityTransform = transform;

        if(ResizeEntity != null)
            ResizeEntity.OriginalSize = EntityTransform.localScale;
    }

    public virtual void AnimationStateMachine()
    {

    }

    public virtual void MovementLogic()
    {

    }

    public virtual void RotationLogic()
    {

    }

    public virtual void Resize(Vector3 _size, bool _lerp = false)
    {
        if (ResizeEntity != null)
        {
            if(_lerp)
            {
                ResizeEntity.Resize(EntityTransform, _size, ResizeSpeed);
                return;
            }

            ResizeEntity.InstantResize(EntityTransform, _size);
        }
    }

    public virtual void RestoreSize(Vector3 _size, bool _lerp = false)
    {
        if (ResizeEntity != null)
        {
            if (_lerp)
            {
                ResizeEntity.RestoreSize(EntityTransform, ResizeSpeed);
                return;
            }

            ResizeEntity.InstantRestoreSize(EntityTransform);
        }
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
