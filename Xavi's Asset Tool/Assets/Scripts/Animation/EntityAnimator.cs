using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimator : MonoBehaviour
{
    public List<AnimationList> Animations;
    public Animator AnimatorEntity;

    public void SwitchAnimationState(string _state)
    {
        DisableAllAnimations();

        AnimationList animation = GetAnimation(_state);

        if (animation != null)
            animation.EnableAnimation(AnimatorEntity);
    }

    public void DisableAllAnimations()
    {
        foreach(AnimationList animation in Animations)
        {
            animation.DisableAnimation(AnimatorEntity);
        }
    }

    AnimationList GetAnimation(string _state)
    {
        AnimationList animationQuery = null;

        foreach (AnimationList animation in Animations)
        {
            if (animation.key.Equals(_state))
            {
                animationQuery = animation;
                break;
            }
        }

        return animationQuery;
    }
}

[System.Serializable]
public class AnimationList 
{
    public string key; 
    public bool enabled;

    public void EnableAnimation(Animator _animator) 
    {
        enabled = true;
        _animator.SetBool(key, enabled);
    }

    public void DisableAnimation(Animator _animator)
    {
        enabled = false;
        _animator.SetBool(key, enabled);
    }
}
