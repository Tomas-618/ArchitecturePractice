using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Source.Data;
using Source.Services.Progress.Contracts;
using UnityEngine;

namespace Source.Services.Progress
{
    public class BinarySaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Save";

        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IProgressRegisterService _progressRegisterService;
        private readonly BinaryFormatter _binaryFormatter;

        [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
        public BinarySaveLoadService(IPersistentProgressService persistentProgressService,
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

            _progressRegisterService.Update(progress);

            string path = BuildPath(ProgressKey);

            using var stream = File.Open(path, FileMode.OpenOrCreate);

            _binaryFormatter.Serialize(stream, progress);
        }

        public async UniTask<PlayerProgress> LoadAsync(CancellationToken cancellationToken)
        {
            string path = BuildPath(ProgressKey);

            if (File.Exists(path) == false)
                return null;

            await using var stream = new FileStream(
                path,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read,
                bufferSize: 4096,
                useAsync: true);

            return await UniTask.RunOnThreadPool(() =>
                    _binaryFormatter.Deserialize(stream) as PlayerProgress,
                cancellationToken: cancellationToken);
        }

        private string BuildPath(string key) =>
            Path.Combine(Application.persistentDataPath, key);
    }
}
