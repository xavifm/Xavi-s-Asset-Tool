using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody EntityRb;
    public Transform EntityTransform;
    public EntityAnimator AnimatorEntity;
    
    public EntityResize ResizeEntity;
    public float ResizeSpeed = 2;

    [HideInInspector] public string CollisionTag;
    [HideInInspector] public bool Colliding;

    private RigidbodyConstraints OriginalConstraints;
    [SerializeField] Collider[] ColliderList;

    void Start()
    {
        EntityTransform = transform;
        OriginalConstraints = EntityRb.constraints;

        if(ResizeEntity != null)
            ResizeEntity.OriginalSize = EntityTransform.localScale;
    }

    public virtual void AnimationLogic()
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

    public virtual void RestoreSize(bool _lerp = false)
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

    public void EnablePhysics()
    {
        EntityRb.constraints = OriginalConstraints;
        EnableAllCollisions();
    }

    public void DisablePhysics()
    {
        EntityRb.constraints = RigidbodyConstraints.FreezeAll;
        DisableAllCollisions();
    }

    protected bool CheckCollisionWithTag(string _tag)
    {
        if(!CollisionTag.Equals(string.Empty) && Colliding && CollisionTag.Equals(_tag))
            return true;

        return false;
    }

    private void DisableAllCollisions()
    {
        foreach(Collider collision in ColliderList)
        {
            collision.enabled = false;
        }
    }

    private void EnableAllCollisions()
    {
        foreach (Collider collision in ColliderList)
        {
            collision.enabled = true;
        }
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
