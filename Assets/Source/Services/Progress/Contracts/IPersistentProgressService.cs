using Source.Data;

namespace Source.Services.Progress.Contracts
{
    public interface IPersistentProgressService
    {
        PlayerProgress Progress { get; set; }
    }
}
