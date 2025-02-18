using System;
using Assets.Sources.Initialization.Di;
using Assets.Sources.Initialization.StateMachine.Contracts;
using Assets.Sources.Initialization.StateMachine.States.Contracts;
using Assets.Sources.Services;
using Assets.Sources.Services.AssetsManagement;
using Assets.Sources.Services.Factories;

namespace Assets.Sources.Initialization.StateMachine.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initial";
        private const string LaboratoryScene = "Laboratory";

        private readonly IGameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly DiContainer _diContainer;

        public BootstrapState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, DiContainer diContainer)
        {
            _gameStateMachine = gameStateMachine ?? throw new ArgumentNullException(nameof(gameStateMachine));
            _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
            _diContainer = diContainer ?? throw new ArgumentNullException(nameof(diContainer));

            RegisterServices();
        }

        public void Enter() =>
            _sceneLoader.LoadAsync(InitialScene, EnterLoadLevel);

        public void Exit() { }

        private void RegisterServices()
        {
            _diContainer.RegisterSingle(new InputService());
            _diContainer.RegisterSingle(new AssetProvider());
            _diContainer.RegisterSingle(new GameObjectsFactory());
        }

        private void EnterLoadLevel() =>
            _gameStateMachine.Enter<LoadLevelState, string>(LaboratoryScene);
    }
}
