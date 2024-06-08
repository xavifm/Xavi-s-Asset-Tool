using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimator : MonoBehaviour
{
    List<AnimationList> Animations;
    Animator AnimatorEntity;

    public void SwitchAnimationState(string _state)
    {

    }

    void DisableAllAnimations()
    {

    }

    AnimationList GetAnimation(string _state)
    {
        return new AnimationList();
    }
}

class AnimationList 
{
    string key; 
    bool enabled;
}
