using JetBrains.Annotations;
using Source.Data;
using Source.Data.Contracts;
using Source.Services.Progress.Contracts;

namespace Source.Services.Progress
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class ActiveScene : IProgressSaver, IActiveScene
    {
        public string Name { get; private set; }

        public void Update(string progressName) =>
            Name = progressName;

        public void LoadProgress(IReadOnlyPlayerProgress progress) =>
            Update(progress.SceneName);

        public void UpdateProgress(PlayerProgress progress) =>
            progress.SceneName = Name;
    }
}
