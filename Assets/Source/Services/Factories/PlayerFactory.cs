using System;
using Source.Components.Player;
using Source.Services.AssetManagement.Constants;
using Source.Services.AssetManagement.Contracts;
using UnityEngine;

namespace Source.Services.Factories
{
    public class PlayerFactory
    {
        private readonly IAssetProvider _assetProvider;

        public PlayerFactory(IAssetProvider assetProvider) =>
            _assetProvider = assetProvider ?? throw new ArgumentNullException(nameof(assetProvider));

        public PlayerRotator Create(Vector3 position, Quaternion rotation,
            Transform parent = null)
        {
            PlayerRotator playerPrefab = _assetProvider.LoadPrefab<PlayerRotator>
                (AssetsPaths.PlayerPath);

            return UnityEngine.Object.Instantiate(playerPrefab, position, rotation, parent);
        }
    }
}
