using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Source.Data;
using Source.Services.AssetManagement.Constants;
using Source.Services.AssetManagement.Contracts;
using Source.Services.Factories.Contracts;
using UnityEngine;
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

        public async UniTask<Canvas> CreateAsync(IObjectResolver container, SpawnData spawnData,
            CancellationToken token)
        {
            var canvasPrefab = await _assetProvider.LoadAsync<Canvas>(AssetsPaths.HudPath, token);

            return container.Instantiate(canvasPrefab, spawnData.Position, spawnData.Rotation,
                spawnData.Parent);
        }
    }
}
