using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovementEntity : CreatureMovement
{
    protected enum AnimationStates { WALK, DAMAGE, DEAD, ATTACK }
    protected Dictionary<AnimationStates, string> Animations;
    protected AnimationStates CurrentAnimation;

    [SerializeField] protected float RotationSpeed;
    [SerializeField] private float DamageTime;

    public override void AnimationLogic()
    {
        if (Animations == null)
            InitializeList();

        if (Animations.TryGetValue(CurrentAnimation, out string animationQuery))
            AnimatorEntity.SwitchAnimationState(animationQuery);

        AnimatorEntity.SwitchCharacterSpeedBlend(EntityRb.velocity.magnitude);
    }

    public override void DamageEntity()
    {
        StartCoroutine(DamageCorroutine());
    }

    public virtual void InitializeList()
    {
        CurrentAnimation = AnimationStates.WALK;
        Animations = new Dictionary<AnimationStates, string>();

        Animations.Add(AnimationStates.WALK, "Walk");
        Animations.Add(AnimationStates.DAMAGE, "Damaged");
        Animations.Add(AnimationStates.DEAD, "Dead");
        Animations.Add(AnimationStates.ATTACK, "Attack");
    }

    private IEnumerator DamageCorroutine()
    {
        CurrentAnimation = AnimationStates.DAMAGE;

        yield return new WaitForSeconds(DamageTime);

        CurrentAnimation = AnimationStates.WALK;
    }
}
