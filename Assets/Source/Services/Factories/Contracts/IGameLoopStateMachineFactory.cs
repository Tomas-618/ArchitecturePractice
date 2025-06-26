using Source.Infrastructure.StateMachine.LifeCycle;

namespace Source.Services.Factories.Contracts
{
    public interface IGameLoopStateMachineFactory
    {
        GameLoopStateMachine Create();
    }
}
