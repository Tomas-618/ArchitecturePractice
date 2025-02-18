namespace Assets.Sources.Initialization.StateMachine.States.Contracts
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}
