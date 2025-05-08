using JetBrains.Annotations;
using Source.Data;
using Source.Services.Progress.Contracts;

namespace Source.Services.Progress
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}
