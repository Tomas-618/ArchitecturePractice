namespace Source.Infrastructure.StateMachine.States.Contracts
{
    public interface IPayloadedState<in TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}
