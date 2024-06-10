using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimator : MonoBehaviour
{
    public List<AnimationList> Animations;
    public Animator AnimatorEntity;

    public void SwitchAnimationState(string _state)
    {

    }

    public void DisableAllAnimations()
    {

    }

    AnimationList GetAnimation(string _state)
    {
        return new AnimationList();
    }
}

[System.Serializable]
public class AnimationList 
{
    public string key; 
    public bool enabled;
}
