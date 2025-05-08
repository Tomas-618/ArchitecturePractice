using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Source.Components.Player;
using Source.Data;
using Source.Services.AssetManagement.Constants;
using Source.Services.AssetManagement.Contracts;
using Source.Services.Factories.Contracts;
using Source.Services.Progress.Contracts;
using VContainer;
using VContainer.Unity;

namespace Source.Services.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IProgressRegisterService _progressRegisterService;

        [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
        public PlayerFactory(IAssetProvider assetProvider, IProgressRegisterService progressRegisterService)
        {
            _assetProvider = assetProvider ?? throw new ArgumentNullException(nameof(assetProvider));
            _progressRegisterService = progressRegisterService ??
                                       throw new ArgumentNullException(nameof(progressRegisterService));
        }

        public async UniTask<PlayerPrefab> CreateAsync(IObjectResolver container, SpawnData spawnData, CancellationToken token)
        {
            var playerPrefab = await _assetProvider.LoadAsync<PlayerPrefab>
                (AssetsPaths.PlayerPath, token);

            var player = container.Instantiate(playerPrefab, spawnData.Position,
                spawnData.Rotation, spawnData.Parent);

            _progressRegisterService.RegisterChildrenWatchers(player.gameObject);

            return player;
        }
    }
}
