using Source.Data;
using System.Collections.Generic;
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

        public void RegistChildrenWatchers(GameObject gameObject)
        {
            foreach (IProgressLoader children in gameObject.GetComponentsInChildren<IProgressLoader>())
                Regist(children);
        }

        public void Clear()
        {
            _loaders.Clear();
            _savers.Clear();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            foreach (IProgressSaver progressSaver in _savers)
                progressSaver.UpdateProgress(progress);
        }

        public void Load(PlayerProgress progress)
        {
            foreach (IProgressLoader progressLoader in _loaders)
                progressLoader.LoadProgress(progress);
        }

        private void Regist(IProgressLoader loader)
        {
            if (loader is IProgressSaver saver)
                _savers.Add(saver);

            _loaders.Add(loader);
        }
    }
}
