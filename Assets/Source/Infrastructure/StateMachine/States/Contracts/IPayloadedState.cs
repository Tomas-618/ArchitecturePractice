namespace Source.Infrastructure.StateMachine.States.Contracts
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}
