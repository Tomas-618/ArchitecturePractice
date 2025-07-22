using System;
using Source.Infrastructure.StateMachine.LifeCycle;

namespace Source.Components.Player.StateMachine.Transitions
{
    public class PlayerCrouchToRunTransition : GameLoopTransition
    {
        private readonly PlayerCrouch _playerCrouch;
        private readonly PlayerRun _playerRun;
        private readonly PlayerStamina _playerStamina;

        public PlayerCrouchToRunTransition(PlayerCrouch playerCrouch, PlayerRun playerRun,
            PlayerStamina playerStamina, GameLoopState nextState) : base(nextState)
        {
            _playerCrouch = playerCrouch ?? throw new ArgumentNullException(nameof(playerCrouch));
            _playerRun = playerRun ?? throw new ArgumentNullException(nameof(playerRun));
            _playerStamina = playerStamina ?? throw new ArgumentNullException(nameof(playerStamina));
        }

        protected override bool CheckCondition() =>
            _playerCrouch.IsStanding && _playerRun.CheckRunning()
                                     && _playerStamina.HasRunOut == false;
    }
}
