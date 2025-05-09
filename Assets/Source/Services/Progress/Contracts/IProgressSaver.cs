using Source.Data;

namespace Source.Services.Progress.Contracts
{
    public interface IProgressSaver : IProgressWatcher
    {
        void UpdateProgress(PlayerProgress progress);
    }
}
