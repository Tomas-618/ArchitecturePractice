using System;
using Source.Components.Player;
using Source.Services.AssetManagement.Constants;
using Source.Services.AssetManagement.Contracts;
using Source.Services.Progress.Contracts;
using UnityEngine;

namespace Source.Services.Factories
{
    public class PlayerFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IProgressRegisterService _progressRegisterService;

        public PlayerFactory(IAssetProvider assetProvider, IProgressRegisterService progressRegisterService)
        {
            _assetProvider = assetProvider ?? throw new ArgumentNullException(nameof(assetProvider));
            _progressRegisterService = progressRegisterService
                ?? throw new ArgumentNullException(nameof(progressRegisterService));
        }

        public PlayerRotator Create(Vector3 position, Quaternion rotation,
            Transform parent = null)
        {
            var playerPrefab = _assetProvider.LoadPrefab<PlayerRotator>
                (AssetsPaths.PlayerPath);

            var player = UnityEngine.Object.Instantiate(playerPrefab, position, rotation, parent);

            _progressRegisterService.RegisterChildrenWatchers(player.gameObject);

            return player;
        }
    }
}
