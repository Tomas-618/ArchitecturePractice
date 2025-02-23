using Source.Data;

namespace Source.Services.Progress.Contracts
{
    public interface ISaveLoadService
    {
        void Save();

        bool TryLoad(out PlayerProgress progress);
    }
}
