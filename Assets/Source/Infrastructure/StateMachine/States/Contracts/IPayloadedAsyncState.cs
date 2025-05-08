using System.Threading;
using Cysharp.Threading.Tasks;

namespace Source.Infrastructure.StateMachine.States.Contracts
{
    public interface IPayloadedAsyncState<in TPayload> : IExitableState
    {
        UniTask EnterAsync(TPayload payload, CancellationToken token);
    }
}
