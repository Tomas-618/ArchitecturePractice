using System;
using Source.Components.Camera;
using Source.Components.Curtain;
using Source.Infrastructure.StateMachine.Contracts;
using Source.Infrastructure.StateMachine.States.Contracts;
using Source.Services;
using Source.Services.AssetManagement;
using Source.Services.Factories;
using UnityEngine;

namespace Source.Infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string PlayerPath = "Prefabs/Player";
        private const string InitialPointTag = "InitialPoint";

        private readonly IGameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly CurtainLoader _curtainLoader;
        private readonly GameObjectsFactory _factory;
        private readonly AssetProvider _assetProvider;

        public LoadLevelState(IGameStateMachine stateMachine,
            SceneLoader sceneLoader, CurtainLoader curtainLoader,
            GameObjectsFactory factory, AssetProvider assetProvider)
        {
            _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
            _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
            _curtainLoader = curtainLoader != null ?
                curtainLoader : throw new ArgumentNullException(nameof(curtainLoader));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _assetProvider = assetProvider ?? throw new ArgumentNullException(nameof(assetProvider));
        }

        public void Enter(string sceneName)
        {
            _curtainLoader.Show();
            _sceneLoader.LoadAsync(sceneName, OnLoaded);
        }

        public void Exit() =>
            _curtainLoader.Hide();

        private void OnLoaded()
        {
            Transform initialPoint = GameObject.FindWithTag(InitialPointTag).transform;

            PlayerCameraTarget playerPrefab = _assetProvider.LoadPrefab<PlayerCameraTarget>(PlayerPath);

            PlayerCameraTarget player = _factory.Create(playerPrefab,
                initialPoint.position, initialPoint.rotation);

            SetCameraTarget(player.Target);

            _stateMachine.Enter<GameLoopState>();
        }

        private void SetCameraTarget(Transform target)
        {
            if (Camera.main.TryGetComponent(out CameraFollower cameraFollower) == false)
                return;

            cameraFollower.Follow(target);
        }
    }
}
