using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Source.Services.AssetManagement.Contracts;
using Unity.VisualScripting;
using UnityEngine;

namespace Source.Services.AssetManagement
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class ResourcesAssetProvider : IAssetProvider
    {
        public async UniTask<TAsset> LoadAsync<TAsset>(string path, CancellationToken token) where TAsset : Object
        {
            var asset = await Resources.LoadAsync<TAsset>(path).ToUniTask(cancellationToken: token);

            return asset.GetComponent<TAsset>();
        }
    }
}
