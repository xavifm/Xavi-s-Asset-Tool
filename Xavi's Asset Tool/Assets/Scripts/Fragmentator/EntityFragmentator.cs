using DinoFracture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Explode()
    {
        if (!enabled)
            return;

        foreach (FractureGeometry geometry in Geometry)
        {
            geometry.Fracture().SetCallbackObject(geometry.gameObject);
        }
    }

}
