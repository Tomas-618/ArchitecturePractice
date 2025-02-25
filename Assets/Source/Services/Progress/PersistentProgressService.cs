using Source.Data;
using Source.Services.Progress.Contracts;

namespace Source.Services.Progress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}
