using UnityEngine;

namespace Assets.Sources.Services.AssetsManagement
{
    public class AssetProvider
    {
        public TComponent LoadPrefab<TComponent>(string path) where TComponent : Object =>
            Resources.Load<TComponent>(path);
    }
}
