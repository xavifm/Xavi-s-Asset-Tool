using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [SerializeField] List<Entity> MapEntities;

    private void Start()
    {
        UpdateList();
    }

    public void UpdateList()
    {
        Entity[] entitiesQuery = GetAllMapEntities();
        MapEntities.Clear();

        foreach (Entity element in entitiesQuery)
            MapEntities.Add(element);
    }

    public List<Entity> GetCloseEntities(Entity _current, float _distance, Entity.EntityType _type = Entity.EntityType.DEFAULT)
    {
        List<Entity> entityQuery = new List<Entity>();

        foreach (Entity element in MapEntities)
        {
            if(element != _current)
            {
                float distance = Vector3.Distance(_current.transform.position, element.transform.position);

                if (distance <= _distance 
                    && (_type.Equals(Entity.EntityType.DEFAULT)
                    || (!_type.Equals(Entity.EntityType.DEFAULT) && _type.Equals(element.TypeOfEntity))))
                    entityQuery.Add(element);
            }
        }

        return entityQuery;
    }

    private Entity[] GetAllMapEntities()
    {
        return FindObjectsOfType<Entity>();
    }
}
