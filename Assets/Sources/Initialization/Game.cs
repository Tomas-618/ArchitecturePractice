using System;
using Assets.Sources.Components.Curtain;
using Assets.Sources.Initialization.Contracts;
using Assets.Sources.Initialization.Di;
using Assets.Sources.Initialization.StateMachine;
using Assets.Sources.Initialization.StateMachine.States;
using Assets.Sources.Services;

namespace Assets.Sources.Initialization
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
