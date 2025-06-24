using Source.Components.Camera;
using Source.Components.InitialPoints;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Source.Infrastructure.LifetimeScopes
{
    public class LevelLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerInitialPoint _playerInitialPoint;
        [SerializeField] private PlayerCamera _playerCamera;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_playerInitialPoint);
            builder.RegisterComponent(_playerCamera);
        }
    }
}
