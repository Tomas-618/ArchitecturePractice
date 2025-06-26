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

        public PlayerMovementStateMachineFactory(PlayerSpeed playerSpeed, PlayerCrouch playerCrouch,
            PlayerRun playerRun)
        {
            _playerSpeed = playerSpeed ?? throw new ArgumentNullException(nameof(playerSpeed));
            _playerCrouch = playerCrouch ?? throw new ArgumentNullException(nameof(playerCrouch));
            _playerRun = playerRun ?? throw new ArgumentNullException(nameof(playerRun));
        }

        public GameLoopStateMachine Create()
        {
            var walkState = new PlayerWalkState(_playerSpeed, 2);
            var runState = new PlayerRunState(_playerSpeed, _playerCrouch, 1);
            var crouchState = new PlayerCrouchState(_playerSpeed, _playerRun, _playerCrouch, 2);

            var walkToRun = new PlayerWalkToRunTransition(_playerRun, runState);
            var walkToCrouch = new PlayerWalkToCrouchTransition(_playerCrouch, crouchState);
            var runToWalk = new PlayerRunToWalkTransition(_playerRun, walkState);
            var crouchToWalk = new PlayerCrouchToWalkTransition(_playerCrouch, walkState);
            var crouchToRun = new PlayerCrouchToRunTransition(_playerCrouch, _playerRun, runState);

            walkState.AddTransition(walkToRun);
            walkState.AddTransition(walkToCrouch);

            runState.AddTransition(runToWalk);

            crouchState.AddTransition(crouchToWalk);
            crouchState.AddTransition(crouchToRun);

            return new GameLoopStateMachine(walkState);
        }
    }
}
