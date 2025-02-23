using System;
using Source.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Source.Services.Progress.Contracts;
using UnityEngine;

namespace Source.Services.Progress
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Save";

        private readonly IPersistentProgressService _persistentProgressService;
        private readonly BinaryFormatter _binaryFormatter;

        public SaveLoadService(IPersistentProgressService persistentProgressService)
        {
            _persistentProgressService = persistentProgressService ??
                throw new ArgumentNullException(nameof(persistentProgressService));
            _binaryFormatter = new BinaryFormatter();
        }

        public void Save()
        {
            PlayerProgress progress = _persistentProgressService.Progress;

            _persistentProgressService.Update();

            string path = BuildPath(ProgressKey);

            using FileStream stream = File.Open(path, FileMode.OpenOrCreate);

            _binaryFormatter.Serialize(stream, progress);
        }

        public bool TryLoad(out PlayerProgress progress)
        {
            string path = BuildPath(ProgressKey);

            progress = null;

            if (File.Exists(path) == false)
                return false;

            using FileStream stream = File.Open(path, FileMode.Open);

            progress = _binaryFormatter.Deserialize(stream) as PlayerProgress;

            return progress != null;
        }
        
        private string BuildPath(string key) =>
            Path.Combine(Application.persistentDataPath, key);
    }
}
