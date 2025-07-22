using JetBrains.Annotations;
using Source.Components.Player;
using Source.Components.Player.StateMachine.States;
using Source.Components.Player.StateMachine.Transitions;
using Source.Infrastructure.StateMachine.LifeCycle;
using Source.Services.Factories.Contracts;

namespace Source.Services.Factories
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class PlayerMovementStateMachineFactory : IPlayerMovementStateMachineFactory
    {
        public GameLoopStateMachine Create(PlayerSpeed playerSpeed, PlayerCrouch playerCrouch,
            PlayerRun playerRun, PlayerStamina playerStamina)
        {
            var walkState = new PlayerWalkState(playerSpeed, 2);
            var runState = new PlayerRunState(playerSpeed, playerCrouch, playerStamina, 1);
            var crouchState = new PlayerCrouchState(playerSpeed, playerRun, playerCrouch, 2);

            var walkToRun = new PlayerWalkToRunTransition(playerRun, playerStamina, runState);
            var walkToCrouch = new PlayerWalkToCrouchTransition(playerCrouch, crouchState);
            var runToWalk = new PlayerRunToWalkTransition(playerRun, playerStamina, walkState);
            var crouchToWalk = new PlayerCrouchToWalkTransition(playerCrouch, walkState);
            var crouchToRun = new PlayerCrouchToRunTransition(playerCrouch, playerRun,
                playerStamina, runState);

            walkState.AddTransition(walkToRun);
            walkState.AddTransition(walkToCrouch);

            runState.AddTransition(runToWalk);

            crouchState.AddTransition(crouchToWalk);
            crouchState.AddTransition(crouchToRun);

            return new GameLoopStateMachine(walkState);
        }
    }
}
