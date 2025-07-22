using Source.Components.Player;
using Source.Infrastructure.StateMachine.LifeCycle;

namespace Source.Services.Factories.Contracts
{
    public interface IPlayerMovementStateMachineFactory
    {
        GameLoopStateMachine Create(PlayerSpeed playerSpeed, PlayerCrouch playerCrouch,
            PlayerRun playerRun, PlayerStamina playerStamina);
    }
}
