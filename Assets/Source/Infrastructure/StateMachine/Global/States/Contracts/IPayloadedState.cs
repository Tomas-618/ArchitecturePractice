namespace Source.Infrastructure.StateMachine.Global.States.Contracts
{
    public interface IPayloadedState<in TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}
