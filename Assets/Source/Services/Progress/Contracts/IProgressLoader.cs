using Source.Data.Contracts;

namespace Source.Services.Progress.Contracts
{
    public interface IProgressLoader
    {
        void LoadProgress(IReadOnlyPlayerProgress progress);
    }
}
