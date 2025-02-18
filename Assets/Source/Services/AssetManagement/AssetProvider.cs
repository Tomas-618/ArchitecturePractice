using UnityEngine;

namespace Source.Services.AssetManagement
{
    public class AssetProvider
    {
        public TComponent LoadPrefab<TComponent>(string path) where TComponent : Object =>
            Resources.Load<TComponent>(path);
    }
}
