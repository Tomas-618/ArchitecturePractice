using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Data;
using UnityEngine;
using VContainer;

namespace Source.Services.Factories.Contracts
{
    public interface IHudFactory
    {
        UniTask<Canvas> CreateAsync(IObjectResolver container, SpawnData spawnData,
            CancellationToken token);
    }
}
