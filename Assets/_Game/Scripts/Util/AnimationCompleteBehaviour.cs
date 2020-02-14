using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimationHandler
{
    void OnAnimationBegin();
    void OnAnimationComplete();
}

public class AnimationCompleteBehaviour : StateMachineBehaviour
{
    private bool called = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        var handler = animator.GetComponent<IAnimationHandler>();
        if (handler != null)
        {
            handler.OnAnimationBegin();
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (!called)
        {
            if (stateInfo.normalizedTime >= 1.0f)
            {
                called = true;

                var handler = animator.GetComponent<IAnimationHandler>();
                if (handler != null)
                {
                    handler.OnAnimationComplete();
                }
            }
        }
    }
}
