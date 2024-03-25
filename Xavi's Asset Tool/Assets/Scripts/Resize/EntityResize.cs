using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityResize : MonoBehaviour
{
    [HideInInspector] public Vector3 OriginalSize;

    public void Resize(Transform _object, Vector3 _size, float _velocity)
    {
        _object.localScale = Vector3.Lerp(_object.localScale, _size, Time.deltaTime * _velocity);
    }

    public void RestoreSize(Transform _object, float _velocity)
    {
        _object.localScale = Vector3.Lerp(_object.localScale, OriginalSize, Time.deltaTime * _velocity);
    }

    public void InstantResize(Transform _object, Vector3 _size)
    {
        _object.localScale = _size;
    }

    public void InstantRestoreSize(Transform _object)
    {
        _object.localScale = OriginalSize;
    }
}
