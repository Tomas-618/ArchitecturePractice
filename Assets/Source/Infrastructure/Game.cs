using System;
using Source.Components.Curtain;
using Source.Infrastructure.Contracts;
using Source.Infrastructure.Di;
using Source.Infrastructure.StateMachine;
using Source.Infrastructure.StateMachine.States;
using Source.Services;

namespace Source.Infrastructure
{
    public class Game : IDisposable
    {
        private readonly GameStateMachine _stateMachine;
        private readonly DiContainer _diContainer;

        public Game(ICoroutineRunner coroutineRunner, DiContainer diContainer, CurtainLoader curtainLoader)
        {
            _diContainer = diContainer ?? throw new ArgumentNullException(nameof(diContainer));
            _stateMachine = new GameStateMachine(diContainer, new SceneLoader(coroutineRunner), curtainLoader);

            _stateMachine.Enter<BootstrapState>();
        }

        public void Dispose() =>
            _diContainer.GetSingle<InputService>().Dispose();
    }
}
