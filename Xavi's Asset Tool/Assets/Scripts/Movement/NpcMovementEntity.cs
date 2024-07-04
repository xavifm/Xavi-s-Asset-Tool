using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovementEntity : CreatureMovement
{
    protected enum AnimationStates { WALK, DAMAGE, DEAD, ATTACK }
    protected Dictionary<AnimationStates, string> Animations;
    protected AnimationStates CurrentAnimation;

    public override void AnimationLogic()
    {
        if (Animations == null)
            InitializeList();

        if (Animations.TryGetValue(CurrentAnimation, out string animationQuery))
            AnimatorEntity.SwitchAnimationState(animationQuery);

        AnimatorEntity.SwitchCharacterSpeedBlend(EntityRb.velocity.magnitude);
    }

    public virtual void InitializeList()
    {
        Animations.Add(AnimationStates.WALK, "Walk");
        Animations.Add(AnimationStates.DAMAGE, "Damaged");
        Animations.Add(AnimationStates.DEAD, "Dead");
        Animations.Add(AnimationStates.ATTACK, "Attack");
    }
}
