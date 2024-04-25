using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    [SerializeField] readonly List<object> Services;

    public void AddService<T>(T _service)
    {
        object objectQuery = GetService<T>();
        
        if(objectQuery != null)
            Services.Add(_service);
    }

    public void RemoveService<T>()
    {
        object objectQuery = GetService<T>();

        if (objectQuery != null)
            Services.Remove(objectQuery);
    }

    public T GetService<T>()
    {
        Type type = typeof(T);
        T resultType = default(T);

        foreach (object service in Services)
        {
            if (service.GetType() == type)
            {
                resultType = (T)service;
                break;
            }
        }

        return resultType;
    }
}
