using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Infrastructure.StateMachine.States.Contracts;
using UnityEngine;

namespace Source.Infrastructure.StateMachine.Contracts
{
    public interface IGameStateMachine
    {
        UniTask EnterAsync<TState>(CancellationToken token) where TState : class, IAsyncState;

        UniTask EnterAsync<TPayLoadedState, TPayload>(TPayload payload, CancellationToken token)
            where TPayLoadedState : class, IPayloadedAsyncState<TPayload>;
    }
}
