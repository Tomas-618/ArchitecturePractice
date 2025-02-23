using UnityEngine;

namespace Source.Services.AssetManagement.Contracts
{
    public interface IAssetProvider
    {
        TComponent LoadPrefab<TComponent>(string path) where TComponent : Object;
    }
}
