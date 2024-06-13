using DinoFracture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To make it work, you should place FractureOnCollision and ExplodeOnFracture at the same location as the FractureGeometry component.

public class EntityFragmentator : MonoBehaviour
{
    public bool Enabled;
    [SerializeField] FractureGeometry[] Geometry;

    public void FragmentByCollision(Collision _collision)
    {
        if (!enabled)
            return;

        foreach(FractureGeometry geometry in Geometry)
        {
            geometry.gameObject.GetComponent<FractureOnCollision>().FragmentByColision(_collision);
        }
    }

    public void Fragment()
    {
        if (!enabled)
            return;

        foreach (FractureGeometry geometry in Geometry)
        {
            geometry.Fracture();
        }
    }

    public void Explode(float _radius, float _force)
    {
        if (!enabled)
            return;

        foreach (FractureGeometry geometry in Geometry)
        {
            ExplodeOnFracture explosion = geometry.GetComponent<ExplodeOnFracture>();

            if(explosion != null)
            {
                explosion.Radius = _radius;
                explosion.Force = _force;
                geometry.Fracture().SetCallbackObject(explosion);
            }
        }
    }

}
