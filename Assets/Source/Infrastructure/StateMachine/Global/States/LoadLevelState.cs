using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Components.Curtain;
using Source.Components.InitialPoints;
using Source.Infrastructure.LifetimeScopes;
using Source.Infrastructure.StateMachine.Global.Contracts;
using Source.Infrastructure.StateMachine.Global.States.Contracts;
using Source.Services.Factories;
using Source.Services.Factories.Contracts;
using Source.Services.Progress.Contracts;
using Source.Services.Scenes.Contracts;
using VContainer;
using VContainer.Unity;

namespace Source.Infrastructure.StateMachine.Global.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IProgressRegisterService _progressRegisterService;
        private readonly CurtainLoader _curtainLoader;
        private readonly IPlayerFactory _playerFactory;
        private readonly IHudFactory _hudFactory;

        private CancellationTokenSource _cancellationTokenSource;

        public LoadLevelState(IGameStateMachine stateMachine, ISceneLoader sceneLoader,
            IPersistentProgressService persistentProgressService,
            IProgressRegisterService progressRegisterService,
            IPlayerFactory factory, IHudFactory hudFactory, CurtainLoader curtainLoader)
        {
            _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
            _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
            _persistentProgressService = persistentProgressService ??
                                         throw new ArgumentNullException(nameof(persistentProgressService));
            _progressRegisterService = progressRegisterService ??
                                       throw new ArgumentNullException(nameof(progressRegisterService));
            _playerFactory = factory ?? throw new ArgumentNullException(nameof(factory));
            _hudFactory = hudFactory ?? throw new ArgumentNullException(nameof(hudFactory));
            _curtainLoader = curtainLoader != null
                ? curtainLoader
                : throw new ArgumentNullException(nameof(curtainLoader));
        }

        public void Enter(string sceneName)
        {
            _curtainLoader.Show();
            _progressRegisterService.ClearWatchers();

            LoadGameWorldAsync(sceneName).Forget();
        }

        public void Exit()
        {
            Dispose();
            _curtainLoader.Hide();
        }

        public void Dispose()
        {
            if (_cancellationTokenSource == null)
                return;

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }

        private async UniTaskVoid LoadGameWorldAsync(string sceneName)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            var cancellationToken = _cancellationTokenSource.Token;

            await _sceneLoader.LoadAsync(sceneName, cancellationToken);

            InitGameWorldAsync(cancellationToken).Forget();
        }

        private async UniTaskVoid InitGameWorldAsync(CancellationToken token)
        {
            _progressRegisterService.RegisterActiveSceneService();

            await CreateEntitiesAsync(token);
            InformProgressLoaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressLoaders() =>
            _progressRegisterService.Load(_persistentProgressService.Progress);

        private async UniTask CreateEntitiesAsync(CancellationToken token)
        {
            var container = LifetimeScope.Find<LevelLifetimeScope>().Container;

            var playerInitialPoint = container.Resolve<PlayerInitialPoint>();

            var playerPrefab = await _playerFactory.CreateAsync(container, playerInitialPoint.SpawnData,
                token);

            var playerMovementStateMachineFactory = new PlayerMovementStateMachineFactory(playerPrefab.Speed,
                playerPrefab.Crouch, playerPrefab.Run);

            playerPrefab.MovementLifeCycle.Init(playerMovementStateMachineFactory);

            var hudPrefab = await _hudFactory.CreateAsync(container, playerInitialPoint.SpawnData,
                token);

            hudPrefab.Init(playerPrefab.Stamina);
        }
    }
}
