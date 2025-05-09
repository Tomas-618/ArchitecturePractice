using System;
using JetBrains.Annotations;
using Source.Data;
using Source.Services.Scenes.Contracts;

namespace Source.Services.Scenes
{
    public class ActiveScene : IActiveScene
    {
        [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
        public ActiveScene(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        public string Name { get; private set; }

        public void Set(string progressName) =>
            Name = progressName;

        public void UpdateProgress(PlayerProgress progress) =>
            progress.SceneName = Name;
    }
}
