using UnityEngine;

namespace Source.Components.Player
{
    public class PlayerAnimatorStateReporter : StateMachineBehaviour
    {
        private PlayerAnimator _animator;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            _animator = FindConcreteAnimator(animator);
            _animator.EnterState(stateInfo.shortNameHash);
        }

        private PlayerAnimator FindConcreteAnimator(Animator animator) =>
            _animator ?? animator.GetComponent<PlayerAnimator>();
    }
}
