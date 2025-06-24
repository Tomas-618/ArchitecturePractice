using System.Collections.Generic;
using Source.Components.Player.Constants;
using Source.Components.Player.Data;
using UnityEngine;

namespace Source.Components.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private readonly Dictionary<int, PlayerAnimatorState> _states = new()
        {
            [PlayerAnimationConstants.IdleStateHash] = PlayerAnimatorState.Idle,
            [PlayerAnimationConstants.SitDownStateHash] = PlayerAnimatorState.SitDown,
            [PlayerAnimationConstants.SittingStateHash] = PlayerAnimatorState.Sitting,
            [PlayerAnimationConstants.StandUpStateHash] = PlayerAnimatorState.StandUp
        };

        [SerializeField] private Animator _animator;

        public PlayerAnimatorState CurrentState { get; private set; }

        public void EnterState(int stateHash)
        {
            if (_states.TryGetValue(stateHash, out var state))
                CurrentState = state;
        }

        public void PlaySitAnimation() =>
            _animator.SetBool(PlayerAnimationConstants.IsSittingParamHash, true);

        public void PlayStandAnimation() =>
            _animator.SetBool(PlayerAnimationConstants.IsSittingParamHash, false);
    }
}
