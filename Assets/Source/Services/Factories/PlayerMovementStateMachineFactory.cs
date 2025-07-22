using System;
using Source.Components.Player;
using Source.Components.Player.StateMachine.States;
using Source.Components.Player.StateMachine.Transitions;
using Source.Infrastructure.StateMachine.LifeCycle;
using Source.Services.Factories.Contracts;

namespace Source.Services.Factories
{
    public class PlayerMovementStateMachineFactory : IGameLoopStateMachineFactory
    {
        private readonly PlayerSpeed _playerSpeed;
        private readonly PlayerCrouch _playerCrouch;
        private readonly PlayerRun _playerRun;
        private readonly PlayerStamina _playerStamina;

        public PlayerMovementStateMachineFactory(PlayerSpeed playerSpeed, PlayerCrouch playerCrouch,
            PlayerRun playerRun, PlayerStamina playerStamina)
        {
            _playerSpeed = playerSpeed ?? throw new ArgumentNullException(nameof(playerSpeed));
            _playerCrouch = playerCrouch ?? throw new ArgumentNullException(nameof(playerCrouch));
            _playerRun = playerRun ?? throw new ArgumentNullException(nameof(playerRun));
            _playerStamina = playerStamina ?? throw new ArgumentNullException(nameof(playerStamina));
        }

        public GameLoopStateMachine Create()
        {
            var walkState = new PlayerWalkState(_playerSpeed, 2);
            var runState = new PlayerRunState(_playerSpeed, _playerCrouch, _playerStamina, 1);
            var crouchState = new PlayerCrouchState(_playerSpeed, _playerRun, _playerCrouch, 2);

            var walkToRun = new PlayerWalkToRunTransition(_playerRun, _playerStamina, runState);
            var walkToCrouch = new PlayerWalkToCrouchTransition(_playerCrouch, crouchState);
            var runToWalk = new PlayerRunToWalkTransition(_playerRun, _playerStamina, walkState);
            var crouchToWalk = new PlayerCrouchToWalkTransition(_playerCrouch, walkState);
            var crouchToRun = new PlayerCrouchToRunTransition(_playerCrouch, _playerRun,
                _playerStamina, runState);

            walkState.AddTransition(walkToRun);
            walkState.AddTransition(walkToCrouch);

            runState.AddTransition(runToWalk);

            crouchState.AddTransition(crouchToWalk);
            crouchState.AddTransition(crouchToRun);

            return new GameLoopStateMachine(walkState);
        }
    }
}
