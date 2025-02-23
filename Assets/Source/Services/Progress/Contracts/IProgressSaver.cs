using Source.Data;

namespace Source.Services.Progress.Contracts
{
    public interface IProgressSaver : IProgressLoader
    {
        void UpdateProgress(PlayerProgress progress);
    }
}
