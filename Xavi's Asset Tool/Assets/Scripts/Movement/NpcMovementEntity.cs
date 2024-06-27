using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovementEntity : CreatureMovement
{
    public override void AnimationLogic()
    {
        AnimatorEntity.SwitchAnimationState("Walk");
        AnimatorEntity.SwitchCharacterSpeedBlend(EntityRb.velocity.magnitude);
    }
}
