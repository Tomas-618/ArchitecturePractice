using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Source.Components.Hud;
using Source.Components.Player;
using Source.Data;
using Source.Services.AssetsManagement.Constants;
using Source.Services.AssetsManagement.Contracts;
using Source.Services.Factories.Contracts;
using VContainer;
using VContainer.Unity;

namespace Source.Services.Factories
{
    public class HudFactory : IHudFactory
    {
        private readonly IAssetProvider _assetProvider;

        [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
        public HudFactory(IAssetProvider assetProvider) =>
            _assetProvider = assetProvider ?? throw new ArgumentNullException(nameof(assetProvider));

        public async UniTask<HudPrefab> CreateAsync(IObjectResolver container, SpawnData spawnData,
            PlayerStamina playerStamina, CancellationToken token)
        {
            var canvasPrefab = await _assetProvider.LoadAsync<HudPrefab>(AssetsPaths.HudPath, token);

            var hudPrefab = container.Instantiate(canvasPrefab, spawnData.Position, spawnData.Rotation,
                spawnData.Parent);

            hudPrefab.Init(playerStamina);

            return hudPrefab;
        }
    }
}
