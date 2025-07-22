using System;
using Source.Infrastructure.StateMachine.LifeCycle;

namespace Source.Components.Player.StateMachine.Transitions
{
    public class PlayerWalkToRunTransition : GameLoopTransition
    {
        private readonly PlayerRun _playerRun;
        private readonly PlayerStamina _playerStamina;

        public PlayerWalkToRunTransition(PlayerRun playerRun, PlayerStamina playerStamina,
            GameLoopState nextState) : base(nextState)
        {
            _playerRun = playerRun ?? throw new ArgumentNullException(nameof(playerRun));
            _playerStamina = playerStamina ?? throw new ArgumentNullException(nameof(playerStamina));
        }

        protected override bool CheckCondition() =>
            _playerRun.CheckRunning() && _playerStamina.HasRunOut == false;
    }
}
