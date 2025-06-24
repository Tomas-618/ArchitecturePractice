using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Components.Hud;
using Source.Data;
using VContainer;

namespace Source.Services.Factories.Contracts
{
    public interface IHudFactory
    {
        UniTask<HudPrefab> CreateAsync(IObjectResolver container, SpawnData spawnData,
            CancellationToken token);
    }
}
