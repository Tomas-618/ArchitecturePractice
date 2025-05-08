using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Infrastructure.StateMachine.States.Contracts;
using UnityEngine;

namespace Source.Infrastructure.StateMachine.States
{
    public class GameLoopState : IAsyncState
    {
        public async UniTask EnterAsync(CancellationToken token)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Exit()
        {
        }
    }
}
