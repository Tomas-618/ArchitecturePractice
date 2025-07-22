using System;
using Source.Infrastructure.StateMachine.LifeCycle;

namespace Source.Components.Player.StateMachine.States
{
    public class PlayerRunState : GameLoopState
    {
        private readonly PlayerSpeed _playerSpeed;
        private readonly PlayerCrouch _playerCrouch;
        private readonly PlayerStamina _playerStamina;

        public PlayerRunState(PlayerSpeed playerSpeed, PlayerCrouch playerCrouch,
            PlayerStamina playerStamina, int transitionsCapacity) : base(transitionsCapacity)
        {
            _playerSpeed = playerSpeed ?? throw new ArgumentNullException(nameof(playerSpeed));
            _playerCrouch = playerCrouch ?? throw new ArgumentNullException(nameof(playerCrouch));
            _playerStamina = playerStamina ?? throw new ArgumentNullException(nameof(playerStamina));
        }

        public override void Enter()
        {
            _playerSpeed.SetOnRunning();
            _playerCrouch.enabled = false;
        }

        public override void Update()
        {
            _playerStamina.Reduce(5f);
        }

        public override void Exit()
        {
            _playerStamina.StartRestoring();
            _playerCrouch.enabled = true;
        }
    }
}
