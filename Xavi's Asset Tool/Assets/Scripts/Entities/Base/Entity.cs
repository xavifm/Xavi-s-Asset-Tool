using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    public enum EntityType { DEFAULT, CREATURE_PLAYER, CREATURE_NPC, WORLD_PROP, WEAPON }

    public EntityType TypeOfEntity;
    public string Name;
    public Text Description;
    public float Life;
    public bool IsKillable;
    public float TimerDisapear;
    public Movement MovementLogic;
    public bool StoredItem;

    private float MaxLife;

    void Start()
    {
        MaxLife = Life;
    }

    void Update()
    {
        VirtualUpdate();
    }

    public virtual void VirtualUpdate()
    {
        LifeLogic();
    }

    public virtual void LifeLogic()
    {
        if (IsKillable && MovementLogic.LifeToRemove > 0)
        {
            Life = Mathf.Clamp(Life - MovementLogic.LifeToRemove, 0, MaxLife);
            MovementLogic.LifeToRemove = 0;

            if (Life <= 0) MovementLogic.Destroy(TimerDisapear);
        }
    }

    public virtual void InteractWith()
    {

    }
}
