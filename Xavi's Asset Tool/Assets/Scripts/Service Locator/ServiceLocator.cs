using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    [SerializeField] List<Service> Services;

    public void AddService(Service _service)
    {
        if (!Services.Contains(_service))
            Services.Add(_service);
    }

    public void RemoveService(Service _service)
    {
        Services.Remove(_service);
    }

    public T GetService<T>() where T : Service
    {
        Type type = typeof(T);
        T resultType = default(T);

        foreach (Service service in Services)
        {
            if (service.GetType() == type)
            {
                resultType = service as T;
                break;
            }
        }

        return resultType;
    }
}

