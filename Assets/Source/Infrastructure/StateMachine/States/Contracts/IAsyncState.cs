using System.Threading;
using Cysharp.Threading.Tasks;

namespace Source.Infrastructure.StateMachine.States.Contracts
{
    public interface IAsyncState : IExitableState
    {
        UniTask EnterAsync(CancellationToken token);
    }
}
