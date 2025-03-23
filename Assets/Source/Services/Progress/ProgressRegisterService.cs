using System.Collections.Generic;
using Source.Data;
using Source.Services.Progress.Contracts;
using UnityEngine;

namespace Source.Services.Progress
{
    public class ProgressRegisterService : IProgressRegisterService
    {
        private readonly List<IProgressLoader> _loaders;
        private readonly List<IProgressSaver> _savers;

        public ProgressRegisterService()
        {
            _loaders = new List<IProgressLoader>();
            _savers = new List<IProgressSaver>();
        }

        public void RegisterChildrenWatchers(GameObject gameObject)
        {
            foreach (var children in gameObject.GetComponentsInChildren<IProgressLoader>())
                Register(children);
        }

        public void Clear()
        {
            _loaders.Clear();
            _savers.Clear();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            foreach (var progressSaver in _savers)
                progressSaver.UpdateProgress(progress);
        }

        public void Load(PlayerProgress progress)
        {
            foreach (var progressLoader in _loaders)
                progressLoader.LoadProgress(progress);
        }

        private void Register(IProgressLoader loader)
        {
            if (loader is IProgressSaver saver)
                _savers.Add(saver);

            _loaders.Add(loader);
        }
    }
}
