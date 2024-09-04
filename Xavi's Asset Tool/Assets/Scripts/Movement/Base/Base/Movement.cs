using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody EntityRb;
    public Transform EntityTransform;
    public Transform ModelTransform;
    public EntityAnimator AnimatorEntity;
    
    public EntityResize ResizeEntity;
    public EntityFragmentator FragmentEntity;
    public float ResizeSpeed = 2;

    [HideInInspector] public string CollisionTag;
    [HideInInspector] public bool Colliding;
    [HideInInspector] public float LifeToRemove;

    protected Transform ColliderEntity;

    private RigidbodyConstraints OriginalConstraints;
    [SerializeField] Collider[] ColliderList;
    protected EntityManager EntityManager;

    void Start()
    {
        EntityTransform = transform;
        OriginalConstraints = EntityRb.constraints;

        EntityManager = GameObject.FindObjectOfType<EntityManager>();

        if (ResizeEntity != null)
        {
            ResizeEntity.OriginalEntitySize = EntityTransform.localScale;

            if(ModelTransform != null)
                ResizeEntity.OriginalModelSize = ModelTransform.localScale;
        }
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

        if (EntityManager != null)
            ImplodeAround(_radius, _force);

        if (FragmentEntity != null)
            FragmentEntity.Explode(_radius, _force);
    }

    public virtual void Resize(Vector3 _size, bool _lerp = false, bool _modelResize = false)
    {
        if (ResizeEntity != null)
        {
            if(_lerp)
            {
                if(_modelResize && ModelTransform != null)
                    ResizeEntity.Resize(ModelTransform, _size, ResizeSpeed);
                else
                    ResizeEntity.Resize(EntityTransform, _size, ResizeSpeed);

                return;
            }

            if(_modelResize && ModelTransform != null)
                ResizeEntity.InstantResize(ModelTransform, _size);
            else
                ResizeEntity.InstantResize(EntityTransform, _size);
        }
    }

    public virtual void RestoreSize(bool _lerp = false, bool _modelResize = false)
    {
        if (ResizeEntity != null)
        {
            if (_lerp)
            {
                if(_modelResize && ModelTransform != null)
                    ResizeEntity.RestoreSize(ModelTransform, ResizeSpeed, _modelResize: true);
                else
                    ResizeEntity.RestoreSize(EntityTransform, ResizeSpeed);

                return;
            }

            if (_modelResize && ModelTransform != null)
                ResizeEntity.InstantRestoreSize(ModelTransform, _modelResize: true);
            else
                ResizeEntity.InstantRestoreSize(EntityTransform);
        }
    }

    public virtual Entity CheckPointingEntity(Transform _rayCastPoint = null, float _distance = 0)
    {
        Entity objectExtracted = null;

        RaycastHit hit;

        Transform rayCastPoint = EntityTransform;

        if (_rayCastPoint != null)
            rayCastPoint = _rayCastPoint;

        if (Physics.Raycast(rayCastPoint.position, rayCastPoint.TransformDirection(Vector3.forward), out hit, _distance))
            objectExtracted = hit.collider.GetComponent<Entity>();

        return objectExtracted;
    }

    public virtual void DamageEntity(float _quantity = 0f)
    {
        RemoveLife(_quantity);
    }

    public void RemoveLife(float _quantity)
    {
        LifeToRemove = _quantity;
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

    public void ResetCollisionInfo()
    {
        CollisionTag = "";
        ColliderEntity = null;
        Colliding = false;
    }

    protected bool CheckCollisionWithTag(string _tag)
    {
        if(!CollisionTag.Equals(string.Empty) && Colliding && CollisionTag.Equals(_tag))
            return true;

        return false;
    }

    protected Entity GetObjectEntity(Transform _object)
    {
        Entity entityQuery = null;

        if (_object.parent != null)
            entityQuery = _object.parent.GetComponent<Entity>();
        else
            entityQuery = _object.GetComponent<Entity>();

        return entityQuery;
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

    private void ImplodeAround(float _radius, float _force)
    {
        List<Entity> closeEntities = EntityManager.GetCloseEntities(GetComponent<Entity>(), _radius);

        foreach (Entity entity in closeEntities)
        {
            Vector3 implodeVector = entity.transform.position - transform.position;
            entity.MovementLogic.DamageEntity(_radius * _force);
            entity.MovementLogic.EntityRb.velocity = implodeVector * _force;
        }
    }

    public virtual void OnCollisionStay(Collision collision)
    {
        CollisionTag = collision.gameObject.tag;
        ColliderEntity = collision.transform;
        Colliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        ResetCollisionInfo();
    }

    IEnumerator DestroyCorroutine(float _timer)
    {
        yield return new WaitForSeconds(_timer);

        Destroy(gameObject);
    }
}
