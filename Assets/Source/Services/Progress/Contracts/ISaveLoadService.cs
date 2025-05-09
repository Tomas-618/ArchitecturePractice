using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Data;

namespace Source.Services.Progress.Contracts
{
    public interface ISaveLoadService
    {
        void Save();

        UniTask<PlayerProgress> LoadAsync(CancellationToken cancellationToken);
    }
}
