using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Components.Player;
using Source.Data;
using VContainer;

namespace Source.Services.Factories.Contracts
{
    public interface IPlayerFactory
    {
        UniTask<PlayerPrefab> CreateAsync(IObjectResolver container, SpawnData spawnData, CancellationToken token);
    }
}
