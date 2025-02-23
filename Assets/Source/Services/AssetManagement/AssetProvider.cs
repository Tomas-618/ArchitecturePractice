using Source.Services.AssetManagement.Contracts;
using UnityEngine;

namespace Source.Services.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public TComponent LoadPrefab<TComponent>(string path) where TComponent : Object =>
            Resources.Load<TComponent>(path);
    }
}
