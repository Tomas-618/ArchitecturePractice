using System;
using JetBrains.Annotations;
using Source.Infrastructure.StateMachine.LifeCycle;

namespace Source.Components.Player.StateMachine.States
{
    public class PlayerCrouchState : GameLoopState
    {
        private readonly PlayerSpeed _playerSpeed;
        private readonly PlayerRun _playerRun;
        private readonly PlayerCrouch _playerCrouch;

        public PlayerCrouchState(PlayerSpeed playerSpeed, PlayerRun playerRun,
            PlayerCrouch playerCrouch, int transitionsCapacity) : base(transitionsCapacity)
        {
            _playerSpeed = playerSpeed ?? throw new ArgumentNullException(nameof(playerSpeed));
            _playerRun = playerRun ?? throw new ArgumentNullException(nameof(playerRun));
            _playerCrouch = playerCrouch ?? throw new ArgumentNullException(nameof(playerCrouch));
        }

        public override void Enter() =>
            _playerSpeed.SetOnCrouching();

        public override void Update()
        {
            if (_playerRun.CheckRunning())
                _playerCrouch.StandUp();
        }
    }
}
