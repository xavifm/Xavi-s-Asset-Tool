using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody EntityRb;
    public Transform EntityTransform;
    public EntityAnimator AnimatorEntity;
    
    public EntityResize ResizeEntity;
    public EntityFragmentator FragmentEntity;
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

    public virtual void Destroy(float _timerRemove, bool _fragmentate = true)
    {
        if(_timerRemove > 0)
            StartCoroutine(DestroyCorroutine(_timerRemove));

        if (FragmentEntity != null && _fragmentate)
            FragmentEntity.Fragment();
    }

    public virtual void Explode(Vector3 _direction, float _radius, float _force)
    {
        EntityRb.velocity = _direction.normalized * _force;

        if (FragmentEntity != null)
            FragmentEntity.Explode();
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

    IEnumerator DestroyCorroutine(float _timer)
    {
        yield return new WaitForSeconds(_timer);

        Destroy(gameObject);
    }
}
