using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum EntityType { CREATURE_PLAYER, CREATURE_NPC, WORLD_PROP, WEAPON }

    public EntityType TypeOfEntity;
}
