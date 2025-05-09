using Source.Data.Contracts;

namespace Source.Services.Progress.Contracts
{
    public interface IProgressLoader : IProgressWatcher
    {
        void LoadProgress(IReadOnlyPlayerProgress progress);
    }
}
