using Source.Infrastructure.StateMachine.States.Contracts;

namespace Source.Infrastructure.StateMachine.Contracts
{
    public interface IGameStateMachine
    {
        void Enter<TState>() where TState : class, IState;

        void Enter<TPayloadedState, TPayload>(TPayload payload)
            where TPayloadedState : class, IPayloadedState<TPayload>;
    }
}
