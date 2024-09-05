using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : Movement
{
    public enum AnimationStates { FLOOR, IDLE, ATTACK, RELOAD, PHYSGUN_GET, PGYSGUN_THROW }
    protected Dictionary<AnimationStates, string> Animations;
    public AnimationStates CurrentAnimation;

    public virtual void InitializeList()
    {
        CurrentAnimation = AnimationStates.FLOOR;
        Animations = new Dictionary<AnimationStates, string>();

        Animations.Add(AnimationStates.FLOOR, "WeaponFloor");
        Animations.Add(AnimationStates.IDLE, "WeaponIdle");
        Animations.Add(AnimationStates.ATTACK, "WeaponShoot");
        Animations.Add(AnimationStates.RELOAD, "WeaponReload");
        Animations.Add(AnimationStates.PHYSGUN_GET, "WeaponPhysgunAttract");
        Animations.Add(AnimationStates.PGYSGUN_THROW, "WeaponPhysgunThrow");
    }

    public void SwitchState(AnimationStates _state)
    {
        CurrentAnimation = _state;
    }

    public override void AnimationLogic(float _blendValue = 0)
    {
        if (Animations == null)
            InitializeList();

        if (Animations.TryGetValue(CurrentAnimation, out string animationQuery))
            AnimatorEntity.SwitchAnimationState(animationQuery);

        AnimatorEntity.SwitchCharacterSpeedBlend(_blendValue);
    }
}
