using System;
using Source.Infrastructure.StateMachine.LifeCycle;

namespace Source.Components.Player.StateMachine.Transitions
{
    public class PlayerCrouchToWalkTransition : GameLoopTransition
    {
        private readonly PlayerCrouch _playerCrouch;

        public PlayerCrouchToWalkTransition(PlayerCrouch playerCrouch, GameLoopState nextState) : base(nextState) =>
            _playerCrouch = playerCrouch ?? throw new ArgumentNullException(nameof(playerCrouch));

        protected override bool CheckCondition() =>
            _playerCrouch.IsStanding;
    }
}
