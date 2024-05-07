using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    public enum EntityType { CREATURE_PLAYER, CREATURE_NPC, WORLD_PROP, WEAPON }

    public EntityType TypeOfEntity;
    public string Name;
    public Text Description;
    public Movement MovementLogic;

    void Start()
    {

    }

    void Update()
    {
        VirtualUpdate();
    }

    public virtual void VirtualUpdate()
    {

    }
}
