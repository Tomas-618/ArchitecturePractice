using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Source.Services.AssetManagement.Contracts
{
    public interface IAssetProvider
    {
        UniTask<TAsset> LoadAsync<TAsset>(string path, CancellationToken token) where TAsset : Object;
    }
}
