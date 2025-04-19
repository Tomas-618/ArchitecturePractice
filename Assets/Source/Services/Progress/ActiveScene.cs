using System;
using Source.Data;
using Source.Data.Contracts;
using Source.Services.Progress.Contracts;

namespace Source.Services.Progress
{
    public class ActiveScene : IProgressSaver, IActiveScene
    {
        public ActiveScene(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        public string Name { get; private set; }

        public void Update(string progressName) =>
            Name = progressName;

        public void LoadProgress(IReadOnlyPlayerProgress progress) =>
            Update(progress.SceneName);

        public void UpdateProgress(PlayerProgress progress) =>
            progress.SceneName = Name;
    }
}
