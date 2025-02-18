using Assets.Sources.Initialization.StateMachine.States.Contracts;

namespace Assets.Sources.Initialization.StateMachine.Contracts
{
    public interface IGameStateMachine
    {
        void Enter<TState>() where TState : class, IState;

        void Enter<TPayloadedState, TPayload>(TPayload payload)
            where TPayloadedState : class, IPayloadedState<TPayload>;
    }
}
