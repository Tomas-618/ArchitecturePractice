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

        public IReadOnlyList<IProgressLoader> Loaders => _loaders;

        public IReadOnlyList<IProgressSaver> Savers => _savers;

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

        private void Regist(IProgressLoader loader)
        {
            if (loader is IProgressSaver saver)
                _savers.Add(saver);

            _loaders.Add(loader);
        }
    }
}
