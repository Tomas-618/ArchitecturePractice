using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Source.Data;
using Source.Services.Progress.Contracts;
using UnityEngine;

namespace Source.Services.Progress
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Save";

        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IProgressRegisterService _progressRegisterService;
        private readonly BinaryFormatter _binaryFormatter;

        public SaveLoadService(IPersistentProgressService persistentProgressService,
            IProgressRegisterService progressRegisterService)
        {
            _persistentProgressService = persistentProgressService ??
                throw new ArgumentNullException(nameof(persistentProgressService));
            _progressRegisterService = progressRegisterService ??
                throw new ArgumentNullException(nameof(progressRegisterService));
            _binaryFormatter = new BinaryFormatter();
        }

        public void Save()
        {
            var progress = _persistentProgressService.Progress;

            _progressRegisterService.UpdateProgress(progress);

            string path = BuildPath(ProgressKey);

            using var stream = File.Open(path, FileMode.OpenOrCreate);

            _binaryFormatter.Serialize(stream, progress);
        }

        public bool TryLoad(out PlayerProgress progress)
        {
            string path = BuildPath(ProgressKey);

            progress = null;

            if (File.Exists(path) == false)
                return false;

            using var stream = File.Open(path, FileMode.Open);

            progress = _binaryFormatter.Deserialize(stream) as PlayerProgress;

            return progress != null;
        }
        
        private string BuildPath(string key) =>
            Path.Combine(Application.persistentDataPath, key);
    }
}
