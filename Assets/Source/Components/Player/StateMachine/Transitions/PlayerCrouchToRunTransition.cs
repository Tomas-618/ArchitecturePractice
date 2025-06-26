using System;
using Source.Infrastructure.StateMachine.LifeCycle;

namespace Source.Components.Player.StateMachine.Transitions
{
    public class PlayerCrouchToRunTransition : GameLoopTransition
    {
        private readonly PlayerCrouch _playerCrouch;
        private readonly PlayerRun _playerRun;

        public PlayerCrouchToRunTransition(PlayerCrouch playerCrouch, PlayerRun playerRun,
            GameLoopState nextState) : base(nextState)
        {
            _playerCrouch = playerCrouch ?? throw new ArgumentNullException(nameof(playerCrouch));
            _playerRun = playerRun ?? throw new ArgumentNullException(nameof(playerRun));
        }

        protected override bool CheckCondition() =>
            _playerCrouch.IsStanding && _playerRun.CheckRunning();
    }
}
