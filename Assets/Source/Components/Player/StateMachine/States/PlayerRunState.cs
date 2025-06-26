using System;
using Source.Infrastructure.StateMachine.LifeCycle;

namespace Source.Components.Player.StateMachine.States
{
    public class PlayerRunState : GameLoopState
    {
        private readonly PlayerSpeed _playerSpeed;
        private readonly PlayerCrouch _playerCrouch;

        public PlayerRunState(PlayerSpeed playerSpeed, PlayerCrouch playerCrouch,
            int transitionsCapacity) : base(transitionsCapacity)
        {
            _playerSpeed = playerSpeed ?? throw new ArgumentNullException(nameof(playerSpeed));
            _playerCrouch = playerCrouch ?? throw new ArgumentNullException(nameof(playerCrouch));
        }

        public override void Enter()
        {
            _playerSpeed.SetOnRunning();
            _playerCrouch.enabled = false;
        }

        public override void Exit() =>
            _playerCrouch.enabled = true;
    }
}
