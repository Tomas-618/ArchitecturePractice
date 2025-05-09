using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Source.Data;
using Source.Data.Contracts;
using Source.Services.Progress.Contracts;
using Source.Services.Scenes.Contracts;
using UnityEngine;

namespace Source.Services.Progress
{
    public class ProgressRegisterService : IProgressRegisterService, IProgressObservable
    {
        private readonly List<IProgressLoader> _loaders;
        private readonly List<IProgressSaver> _savers;
        private readonly IActiveScene _activeScene;

        [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
        public ProgressRegisterService(IActiveScene activeScene)
        {
            _loaders = new List<IProgressLoader>();
            _savers = new List<IProgressSaver>();
            _activeScene = activeScene ?? throw new ArgumentNullException(nameof(activeScene));
        }

        public event Action Saved;

        public void RegisterActiveSceneService() =>
            Register(_activeScene);

        public void RegisterChildrenWatchers(GameObject gameObject)
        {
            var watchers = gameObject
                .GetComponentsInChildren<IProgressWatcher>();

            for (int i = 0; i < watchers.Length; i++)
                Register(watchers[i]);
        }

        public void ClearWatchers()
        {
            _loaders.Clear();
            _savers.Clear();
        }

        public void Update(PlayerProgress progress)
        {
            progress.IsValid = true;

            for (int i = 0; i < _savers.Count; i++)
                _savers[i].UpdateProgress(progress);

            Saved?.Invoke();
        }

        public void Load(IReadOnlyPlayerProgress progress)
        {
            if (progress.IsValid == false || _activeScene.Name != progress.SceneName)
                return;

            for (int i = 0; i < _loaders.Count; i++)
                _loaders[i].LoadProgress(progress);
        }

        private void Register(IProgressWatcher watcher)
        {
            if (watcher is IProgressLoader loader)
                _loaders.Add(loader);

            if (watcher is IProgressSaver saver)
                _savers.Add(saver);
        }
    }
}
