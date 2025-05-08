using System.Threading;
using Cysharp.Threading.Tasks;

namespace Source.Services.Scenes.Contracts
{
    public interface ISceneLoader
    {
        UniTask LoadAsync(string name, CancellationToken cancellationToken);
    }
}
