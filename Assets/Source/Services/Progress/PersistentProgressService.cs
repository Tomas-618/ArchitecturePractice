using System;
using Source.Data;
using Source.Services.Progress.Contracts;
using UnityEngine;

namespace Source.Services.Progress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        private readonly IProgressRegisterService _progressRegisterService;

        public PersistentProgressService(IProgressRegisterService progressRegisterService) =>
            _progressRegisterService = progressRegisterService ??
            throw new ArgumentNullException(nameof(progressRegisterService));

        public PlayerProgress Progress { get; set; }

        public void Update()
        {
            foreach (IProgressSaver progressSaver in _progressRegisterService.Savers)
                progressSaver.UpdateProgress(Progress);
        }

        public void Load()
        {
            foreach (IProgressLoader progressLoader in _progressRegisterService.Loaders)
                progressLoader.LoadProgress(Progress);
        }

        public void Regist(GameObject gameObject) =>
            _progressRegisterService.RegistChildrenWatchers(gameObject);

        public void ClearRegistered() =>
            _progressRegisterService.Clear();
    }
}
