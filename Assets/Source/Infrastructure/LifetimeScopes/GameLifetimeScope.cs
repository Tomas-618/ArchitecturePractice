using Source.Components.Curtain;
using Source.Services.AssetManagement;
using Source.Services.AssetManagement.Contracts;
using Source.Services.Factories;
using Source.Services.Factories.Contracts;
using Source.Services.Input;
using Source.Services.Input.Contracts;
using Source.Services.Progress;
using Source.Services.Progress.Contracts;
using Source.Services.Scenes;
using Source.Services.Scenes.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Source.Infrastructure.LifetimeScopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private CurtainLoader _curtainLoader;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Bootstrapper>();
            builder.RegisterComponentInNewPrefab(_curtainLoader, Lifetime.Singleton)
                .DontDestroyOnLoad();

            RegisterServices(builder);
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<IInputService, InputService>(Lifetime.Singleton);
            builder.Register<IAssetProvider, ResourcesAssetProvider>(Lifetime.Singleton);
            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
            builder.Register<IActiveScene, ActiveScene>(Lifetime.Singleton)
                .WithParameter("name", SceneManager.GetActiveScene().name);
            builder.Register<IPersistentProgressService, PersistentProgressService>(Lifetime.Singleton);
            builder.Register<IProgressRegisterService, ProgressRegisterService>(Lifetime.Singleton);
            builder.Register<ISaveLoadService, BinarySaveLoadService>(Lifetime.Singleton);
            builder.Register<IPlayerFactory, PlayerFactory>(Lifetime.Singleton);
        }
    }
}
