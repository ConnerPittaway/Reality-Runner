using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimators : MonoBehaviour
{
    public List<AnimatorOverrideController> staticAnimators;
    public List<AnimatorOverrideController> fallingAnimators;
    public List<AnimatorOverrideController> flyingAnimators;

    public AnimatorOverrideController GetFlyingAnimator(int index)
    {
        return flyingAnimators[index];
    }
}
