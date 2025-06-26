using System;
using Source.Infrastructure.StateMachine.LifeCycle;

namespace Source.Components.Player.StateMachine.Transitions
{
    public class PlayerRunToWalkTransition : GameLoopTransition
    {
        private readonly PlayerRun _playerRun;

        public PlayerRunToWalkTransition(PlayerRun playerRun, GameLoopState nextState) : base(nextState) =>
            _playerRun = playerRun ?? throw new ArgumentNullException(nameof(playerRun));

        protected override bool CheckCondition() =>
            _playerRun.CheckRunning() == false;
    }
}
