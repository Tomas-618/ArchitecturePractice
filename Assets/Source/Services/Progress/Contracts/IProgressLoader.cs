using Source.Data;

namespace Source.Services.Progress.Contracts
{
    public interface IProgressLoader
    {
        void LoadProgress(PlayerProgress progress);
    }
}
